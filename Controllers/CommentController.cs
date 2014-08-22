using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SocialNetwork.Filters;
using SocialNetwork.Models;
using SocialNetwork.Repository.Interfaces;

namespace SocialNetwork.Controllers
{
    [Culture]
    public class CommentController : Controller
    {
        private ApplicationUserManager userManager;
        private ICommentRepository commentRepository;
        private IUserTaskRepository userTaskRepository;

        public CommentController()
        {
        }

        public CommentController(ICommentRepository commentRepository, IUserTaskRepository userTaskRepository)
        {
            this.commentRepository = commentRepository;
            this.userTaskRepository = userTaskRepository;
        }

        public CommentController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }

        [HttpPost]
        public PartialViewResult AddComment(string comment, int taskId)
        {
            if (!string.IsNullOrWhiteSpace(comment))
            {
                var newComment = new CommentModel()
                {
                    CommentContent = comment,
                    UserTaskId = taskId,
                    UserId = User.Identity.GetUserId(),
                    DateAdded = DateTime.Now
                };
                commentRepository.Add(newComment);
            }
            return PartialView("_CommentViewPartial", GetTasksComments(taskId));
        }

        [HttpGet]
        public ActionResult ViewComment(int? taskId)
        {
            return PartialView("_CommentViewPartial", GetTasksComments(taskId));
        }

        #region Helpers
        private List<CommentsViewModel> GetTasksComments(int? taskId, int blockNumber = 1, int blockSize = 5)
        {
            int startIndex = (blockNumber - 1)*blockSize;
            var taskComments = from comment in commentRepository.GetAll()
                               where comment.UserTaskId == taskId
                               select comment;
            var comments = (from taskComment in taskComments
                            let user = UserManager.FindById(taskComment.UserId)
                            orderby taskComment.DateAdded descending
                            select new CommentsViewModel()
                            {
                                Id = taskComment.Id,
                                Content = taskComment.CommentContent,
                                DateAdded = taskComment.DateAdded,
                                UserId = taskComment.UserId,
                                UserName = user.UserName,
                                UserImageUrl = Helpers.Helpers.TransformImage(user.UserPhotoUrl, 38),
                            }).Skip(startIndex).Take(blockSize);
            return comments.ToList();
        }

        private class JsonModel
        {
            public string HtmlString { get; set; }
            public bool NoMoreData { get; set; }
        }

        [HttpPost]
        public ActionResult InfiniteScroll(int? taskId, int blockNumber)
        {
            System.Threading.Thread.Sleep(1000);
            const int blockSize = 5;
            var comments = GetTasksComments(taskId, blockNumber);
            var jsonModel = new JsonModel
            {
                NoMoreData = comments.Count < blockSize,
                HtmlString = RenderPartialViewToString("_CommentViewPartial", comments),
            };
            return Json(jsonModel);
        }

        private string RenderPartialViewToString(string viewName, List<CommentsViewModel> comments)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.RouteData.GetRequiredString("action");                
            }
            ViewData.Model = comments;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        } 
        #endregion
    }
}