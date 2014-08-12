using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SocialNetwork.Filters;
using SocialNetwork.Helpers;
using SocialNetwork.Models;
using SocialNetwork.Repository.Interfaces;

namespace SocialNetwork.Controllers
{
    [Culture]
    public class UserTaskController : Controller
    {
        private ApplicationUserManager userManager;
        private IUserTaskRepository userTaskRepository;
        private ICommentRepository commentRepository;
        private ILikeRepository likeRepository;
        private IUserSolvedTaskRepository userSolvedTaskRepository;
        private ICategoryRepository categoryRepository;
        private ITagRepository tagRepository;

        public UserTaskController()
        {
        }

        public UserTaskController(IUserTaskRepository userTaskRepository, IUserSolvedTaskRepository userSolvedTaskRepository,
            ICommentRepository commentRepository, ILikeRepository likeRepository, ICategoryRepository categoryRepository,
            ITagRepository tagRepository)
        {
            this.userTaskRepository = userTaskRepository;
            this.userSolvedTaskRepository = userSolvedTaskRepository;
            this.commentRepository = commentRepository;
            this.likeRepository = likeRepository;
            this.categoryRepository = categoryRepository;
            this.tagRepository = tagRepository;
        }

        public UserTaskController(ApplicationUserManager userManager)
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
        // GET: UserTask

        public ActionResult ViewAllTasks()
        {
            var tasks = userTaskRepository.GetAll();
            var allTasks = new List<UserTasksViewAllModel>();
            foreach (var t in tasks)
            {
                var task = new UserTasksViewAllModel()
                {
                    Id = t.Id,
                    UserTaskTitle = t.UserTaskTitle,
                    UserTaskStatus = t.UserTaskStatus,
                    DateAdded = t.DateAdded,
                    UserName = UserManager.FindById(t.UserId).UserName,
                    LikesAmount = t.Likes.Count(),
                    SolutionsAmount = t.SolvedTasks.Count,
                    Tags = t.Tags.ToList(),
                    CommentsAmount = t.Comments.Count,
                    Category = categoryRepository.GetById(t.CategoryId).CategoryName,
                    Content = Conventors.StringConventor(t.UserTaskContent)
                };
                allTasks.Add(task);
            }
            return View(allTasks.OrderBy(x => x.DateAdded).ToList());
        }
    }
}