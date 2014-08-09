using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using SocialNetwork.Filters;
using SocialNetwork.Models;
using SocialNetwork.Repository.Interfaces;

namespace SocialNetwork.Controllers
{
    [Culture]
    public class AdminController : Controller
    {
        private ApplicationUserManager userManager;
        private IUserTaskRepository userTaskRepository;
        private ICommentRepository commentRepository;
        private ILikeRepository likeRepository;
        private IUserSolvedTaskRepository userSolvedTaskRepository;

        public AdminController()
        {
        }

        public AdminController(IUserTaskRepository userTaskRepository, IUserSolvedTaskRepository userSolvedTaskRepository, ICommentRepository commentRepository, ILikeRepository likeRepository)
        {
            this.userTaskRepository = userTaskRepository;
            this.userSolvedTaskRepository = userSolvedTaskRepository;
            this.commentRepository = commentRepository;
            this.likeRepository = likeRepository;
        }

        public AdminController(ApplicationUserManager userManager)
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

        // GET: Admin
        [Authorize(Roles = "admin")]
        public ActionResult AdminControlPanelUsers()
        {
            var users = new Collection<AdminControlPanelUsersViewModel>();
            foreach (var applicationUser in UserManager.Users)
            {
                var user = new AdminControlPanelUsersViewModel()
                {
                    Id = applicationUser.Id,
                    UserName = applicationUser.UserName,
                    Email = applicationUser.Email,
                    TaskAmount = userTaskRepository.GetUserTasksAmountById(applicationUser.Id),
                    AttemptAmount = applicationUser.AttemptAmount,
                    SolutionAmount = userSolvedTaskRepository.GetUserSolvedTasksAmount(applicationUser.Id),
                    UserRate = applicationUser.UserRate,
                    LockoutEnabled = applicationUser.LockoutEnabled,
                    LockoutEndDateUtc = applicationUser.LockoutEndDateUtc,
                };
                users.Add(user);
            }
            ViewBag.TotalUsers = UserManager.Users.Count();
            return View(users);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdminControlPanelTasks()
        {
            var tasks = new Collection<AdminControlPanelTasksViewModel>();
            foreach (var userTask in userTaskRepository.GetAll())
            {
                var task = new AdminControlPanelTasksViewModel()
                {
                    Id = userTask.Id,
                    TaskTitle = userTask.UserTaskTitle,
                    CommentAmount = commentRepository.GetAll(x => x.UserTaskId == userTask.Id).Count,
                    SolutionAmount = userSolvedTaskRepository.GetAll(x => x.UserTaskId == userTask.Id).Count,
                    LikeAmount = likeRepository.GetAll(x => x.UserTaskId == userTask.Id).Count,
                    DateAdded = userTask.DateAdded,
                    TaskStatus = userTask.UserTaskStatus
                };
                tasks.Add(task);
            }
            return View(tasks);
        }
    }
}