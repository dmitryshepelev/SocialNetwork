﻿using System;
﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using SocialNetwork.Filters;
using SocialNetwork.Models;
using SocialNetwork.Repository.Interfaces;

namespace SocialNetwork.Controllers
{
    [Culture]
    public class UserTaskController : Controller
    {
        private ApplicationUserManager userManager;
        private IUserTaskRepository userTaskRepository;
        private ICategoryRepository categoryRepository;
        private ITagRepository tagRepository;
        private ITaskSolutionRepository taskSolutionRepository;
        private IUserTaskTagsRepository userTaskTagsRepository;
        private IUserSolvedTaskRepository userSolvedTasksRepository;
        private ILikeRepository likeRepository;

        public UserTaskController()
        {
        }

        public UserTaskController(IUserTaskRepository userTaskRepository, ICategoryRepository categoryRepository,
            ITagRepository tagRepository, ITaskSolutionRepository taskSolutionRepository, IUserTaskTagsRepository userTaskTagsRepository,
            ILikeRepository likeRepository, IUserSolvedTaskRepository userSolvedTasksRepository)
        {
            this.userTaskRepository = userTaskRepository;
            this.categoryRepository = categoryRepository;
            this.tagRepository = tagRepository;
            this.taskSolutionRepository = taskSolutionRepository;
            this.userTaskTagsRepository = userTaskTagsRepository;
            this.userSolvedTasksRepository = userSolvedTasksRepository;
            this.likeRepository = likeRepository;
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

        [HttpGet]
        public ActionResult TaskStatistics(int? taskId)
        {
            if (taskId == null)
            {
                return null;
            }
            return PartialView("_TaskStatistics", GetTaskStatistics((int)taskId));
        }

        [HttpGet]
        public PartialViewResult LikeValueChanged(bool likeValue, int taskId)
        {
            var userLike = (from like in likeRepository.GetAll()
                            where like.UserTaskId == taskId
                            where like.UserId == User.Identity.GetUserId()
                            select like).FirstOrDefault();
            if (userLike == null)
            {
                userLike = new LikeModel { UserId = User.Identity.GetUserId(), UserTaskId = taskId };
                likeRepository.Add(userLike);
            }
            userLike.LikeValue = likeValue;
            if (!likeValue)
            {
                likeRepository.Delete(userLike);
            }
            else
            {
                likeRepository.Update(userLike);
            }
            return PartialView("_TaskStatistics", GetTaskStatistics((int)taskId));
        }

        [HttpPost]
        public JsonResult CheckSolution(string solutions, int? taskId)
        {
            var taskSolutions = from s in taskSolutionRepository.GetAll() where s.UserTaskId == (int) taskId select s.Solution.ToUpper();
            var user = UserManager.FindById(User.Identity.GetUserId());
            var userTasksSolutions = new UserSolvedTaskModel();
            if (userSolvedTasksRepository.GetUserSolvedTask((int) taskId, user.Id) == null)
            {
                userTasksSolutions.UserId = user.Id;
                userTasksSolutions.UserTaskId = (int) taskId;
                userTasksSolutions.IsSolved = false;
                userSolvedTasksRepository.Add(userTasksSolutions);
            }
            else
            {
                userTasksSolutions = userSolvedTasksRepository.GetUserSolvedTask((int)taskId, user.Id);
            }
            userTasksSolutions.AttemptAmount += 1;
            var solution = solutions.Split(',');
            foreach (var s in solution)
            {
                s.Trim();
                if (taskSolutions.Contains(s.ToUpper())) continue;
                userSolvedTasksRepository.Update(userTasksSolutions);
                user.AttemptAmount += 1;
                UserManager.Update(user);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            userTasksSolutions.IsSolved = true;
            user.AttemptAmount += 1;
            TimeSpan timeDifference = DateTime.Now - userTaskRepository.GetById((int) taskId).DateAdded;
            var addRate = (((1.0 / userTasksSolutions.AttemptAmount) * 0.8) + ((1.0 / timeDifference.TotalHours) * 0.2)) * 100;
            user.UserRate += addRate;
            UserManager.Update(user);
            userSolvedTasksRepository.Update(userTasksSolutions);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewAllTasks(string filterParam, string filterName, string sortParam, bool? sortOrder = false)
        {
            var tasks = userTaskRepository.GetAll();
            tasks = Helpers.Helpers.TasksSort(tasks.ToList(), sortParam, sortOrder).ToList();
            var allTasks = tasks.Select(t => new UserTasksViewAllModel()
            {
                Id = t.Id,
                UserTaskTitle = t.UserTaskTitle,
                UserTaskStatus = t.UserTaskStatus,
                DateAdded = t.DateAdded,
                UserName = UserManager.FindById(t.User.Id).UserName,
                UserId = UserManager.FindById(t.User.Id).Id,
                Tags =
                    (from i in userTaskTagsRepository.GetAll()
                        where i.UserTaskId == t.Id
                        from j in tagRepository.GetAll()
                        where j.Id == i.TagId select j).ToList(),
                Category = categoryRepository.GetById(t.CategoryId).CategoryName,
                Content = Helpers.Helpers.StringConventor(t.UserTaskContent)
            }).ToList();
            ViewBag.SortOrder = !sortOrder;
            ViewBag.FilterParam = filterParam;
            ViewBag.FilterName = filterName;
            ViewBag.Categories = from category in categoryRepository.GetAll()
                orderby category.CategoryName
                select category.CategoryName;
            return View(TaskFilter(allTasks.ToList(), filterParam, filterName));
        }

        [Authorize]
        public ActionResult ViewTask(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var task = userTaskRepository.GetById(id);
            var taskToView = new UserTasksViewModel
            {
                Id = task.Id,
                UserTaskTitle = task.UserTaskTitle,
                Category = categoryRepository.GetById(task.CategoryId).CategoryName,
                Content = task.UserTaskContent,
                DateAdded = task.DateAdded,
                UserId = UserManager.FindById(task.UserId).Id,
                UserName = UserManager.FindById(task.UserId).UserName,
                UserImage = Helpers.Helpers.TransformImage(UserManager.FindById(task.UserId).UserPhotoUrl, 64),
                UserTaskStatus = task.UserTaskStatus,
                CommentsAmount = task.Comments.Count,
                LikesAmount = task.Likes.Count(),
                SolutionsAmount = task.SolvedTasks.Count,
                Tags = (from i in userTaskTagsRepository.GetAll()
                    where i.UserTaskId == task.Id
                    from j in tagRepository.GetAll()
                    where j.Id == i.TagId
                    select j).ToList(),
                IsSolved = userSolvedTasksRepository.GetUserSolvedTask(task.Id, User.Identity.GetUserId()) != null && userSolvedTasksRepository.GetUserSolvedTask(task.Id, User.Identity.GetUserId()).IsSolved
            };
            ViewBag.UserPhoto = Helpers.Helpers.TransformImage(UserManager.FindById(User.Identity.GetUserId()).UserPhotoUrl, 34);
            return View(taskToView);
        }

        [HttpGet]
        public JsonResult GetTags(string q)
        {
            var jsonTags = tagRepository.GetAll().Where(x => x.TagName.ToUpper().Contains(q.ToUpper())).Select(res => new { id = res.Id.ToString(), name = res.TagName }).ToList();
            jsonTags.Add(new { id = q, name = String.Format("Add \"{0}\"", q )});               
            return Json(jsonTags, JsonRequestBehavior.AllowGet);
        }

        class JsonStringResult
        {
            public string url { get; set; }
            public string param { get; set; }
        }

        [HttpPost]
        public JsonResult AddVideo(string urlVideo)
        {
            urlVideo = urlVideo.Replace("watch?v=", "embed/");
            var jsonResult = new JsonStringResult()
            {
                param = String.Format("<iframe width=\"382\" height=\"214\" src=\"{0}\" frameborder=\"0\" allowfullscreen></iframe>", urlVideo)
            };
            jsonResult.url = String.Format("[VIDEO]{0}[/VIDEO]", jsonResult.param);
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UploadImage(HttpPostedFileBase file)
        {
            var image = Helpers.Helpers.UploadImage(file);
            var jsonResult = new JsonStringResult()
            {
                url = String.Format("[IMAGE]<img src=\"{0}\"/>[/IMAGE]", Helpers.Helpers.TransformImage(image.Uri.ToString(), 522, image.Width)),
                param = image.PublicId,
            };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult CreateTask()
        {
            ViewBag.Categories = (from c in categoryRepository.GetAll() orderby c.CategoryName select c.CategoryName).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult CreateTask(CreateTaskViewModel model)
        {
            model.Content = Helpers.Helpers.CreateEquation(model.Content);
            model.Content = Helpers.Helpers.CreateVideo(model.Content);
            model.Content = Helpers.Helpers.CreateImage(model.Content);
            var newTask = new UserTaskModel()
            {
                UserTaskTitle = model.UserTaskTitle,
                CategoryId = categoryRepository.GetId(model.Category),
                DateAdded = DateTime.Now,
                UserTaskContent = model.Content,
                UserId = User.Identity.GetUserId()
            };
            var createdTask = userTaskRepository.Add(newTask);
            var tags = model.Tags.Split(',');
            foreach (var t in tags)
            {
                var tag = new TagModel();
                try
                {
                    tag = tagRepository.GetById(Int32.Parse(t));
                }
                catch (FormatException)
                {
                    tag.TagName = t;
                    tagRepository.Add(tag);
                }
                userTaskTagsRepository.Add(new UserTaskTagModel() { TagId = tag.Id, UserTaskId = createdTask.Id });
            }
            var answers = model.Answers.Split(',');
            foreach (var answer in answers)
            {
                taskSolutionRepository.Add(new TaskSolutionModel() { Solution = answer, UserTaskId = createdTask.Id });
            }
            return RedirectToAction("ViewAllTasks");
        }

        #region Helpers
        public List<UserTasksViewAllModel> TaskFilter(List<UserTasksViewAllModel> tasks, string filterParam, string filterName)
        {
            List<UserTasksViewAllModel> filterResult = null;
            switch (filterParam)
            {
                case "Tag":
                    filterResult = (from task in tasks from tag in task.Tags where tag.TagName == filterName select task).ToList();
                    break;
                case "Category":
                    filterResult = (from task in tasks where task.Category == filterName select task).ToList();
                    break;
                default:
                    filterResult = tasks;
                    break;
            }
            return filterResult;
        }

        private TaskStatisticsViewModel GetTaskStatistics(int taskId)
        {
            var task = userTaskRepository.GetById(taskId);
            var userLike = (from like in likeRepository.GetAll()
                            where like.UserTaskId == taskId
                            where like.UserId == User.Identity.GetUserId()
                            select like).FirstOrDefault();
            var taskStatistics = new TaskStatisticsViewModel()
            {
                CommentsAmount = task.Comments.Count,
                LikesAmount = task.Likes.Count,
                SolutionsAmount = task.SolvedTasks.Count,
                IsLiked = userLike != null && userLike.LikeValue,
                TaskId = taskId
            };
            return taskStatistics;
        }
        #endregion
    }
}