using System;
using System.Collections.Generic;
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
    public class HomeController : Controller
    {
        private ApplicationUserManager userManager;
        private IUserTaskRepository userTaskRepository;
        private ITagRepository tagRepositrory;
        private IUserSolvedTaskRepository userSolvedTaskRepository;
        private IUserTaskTagsRepository userTaskTagsRepositrory;

        public HomeController()
        {
        }

        public HomeController(IUserTaskRepository userTaskRepository, IUserSolvedTaskRepository userSolvedTaskRepository, ITagRepository tagRepositrory,
            IUserTaskTagsRepository userTaskTagsRepositrory)
        {
            this.userTaskRepository = userTaskRepository;
            this.userSolvedTaskRepository = userSolvedTaskRepository;
            this.tagRepositrory = tagRepositrory;
            this.userTaskTagsRepositrory = userTaskTagsRepositrory;
        }

        public HomeController(ApplicationUserManager userManager)
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TopRateUsers()
        {
            var users = UserManager.Users.OrderByDescending(x => x.UserRate).Take(10);
            var topRateUsers = users.Select(user => new TopRateUsersViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Rate = user.UserRate
            }).ToList();
            return PartialView("_TopRateUsersPartial", topRateUsers);
        }

        public ActionResult MostSolvedTasks()
        {
            var tasks = userTaskRepository.GetAll().OrderByDescending(x => x.SolvedTasks.Count).Take(10);
            var mostSolvedTasks = tasks.Select(task => new MostSolvedTaskViewModel()
            {
                TaskId = task.Id,
                TaskTitle = task.UserTaskTitle,
                SolutionsAmount = task.SolvedTasks.Count
            }).ToList();
            return PartialView("_MostSolvedTasksPartial", mostSolvedTasks);
        }

        public ActionResult RecentTasks()
        {
            var tasks = userTaskRepository.GetAll().OrderByDescending(x => x.DateAdded).Take(10);
            var recentTasks = tasks.Select(task => new RecentTaskViewModel()
            {
                TaskId = task.Id,
                TaskTitle = task.UserTaskTitle,
                DateAdded = task.DateAdded
            }).ToList();
            return PartialView("_RecentTasksPartial", recentTasks);
        }

        public ActionResult ViewTagCloud()
        {
            return View();
        }

        public ActionResult TagCloud(int count)
        {
            return PartialView("_TagCloudPartial", GetTags(count));
        }

        #region Helpers

        public List<TagCloudModel> GetTags(int n)
        {
            var tags = tagRepositrory.GetAll();
            if (n == 0)
            {
                n = tags.Count;
            }
            return tags.Select(tag => new TagCloudModel()
            {
                TagId = tag.Id,
                TagName = tag.TagName,
                TagFrequency = (from userTaskTag in userTaskTagsRepositrory.GetAll()
                                where userTaskTag.TagId == tag.Id
                                select userTaskTag).Count()
            }).Take((int)n).ToList();
        }
        #endregion
    }
}