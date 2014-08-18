using System;
using System.Collections.Generic;
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
            var newComment = new CommentModel()
            {
                CommentContent = comment,
                UserTaskId = taskId,
                UserId = User.Identity.GetUserId(),
                DateAdded = DateTime.Now
            };
            commentRepository.Add(newComment);
            return PartialView("_CommentViewPartial", GetTasksComments(taskId));
        }

        [HttpGet]
        public ActionResult ViewComment(int? taskId)
        {
            return PartialView("_CommentViewPartial", GetTasksComments(taskId).Take(5).ToList());
        }

        #region Helpers
        private List<CommentsViewModel> GetTasksComments(int? taskId)
        {
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
                                UserImageUrl = Helpers.Helpers.TransformImage(user.UserPhotoUrl, 38)
                            }).ToList();
            return comments;
        }
        #endregion
    }
}