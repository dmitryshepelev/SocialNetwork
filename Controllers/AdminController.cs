﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    public class AdminController : Controller
    {
        private ApplicationUserManager userManager;
        private IUserTaskRepository userTaskRepository;
        private ICategoryRepository categoryRepository;

        public AdminController()
        {
        }

        public AdminController(IUserTaskRepository userTaskRepository, ICategoryRepository categoryRepository)
        {
            this.userTaskRepository = userTaskRepository;
            this.categoryRepository = categoryRepository;
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
        public ActionResult AdminControlPanelUsers(string sortParam, bool? sortOrder = true)
        {
            var users = new List<AdminControlPanelUsersViewModel>();
            var applicationUsers = UserManager.Users.ToList();
            foreach (var applicationUser in applicationUsers)
            {
                var user = new AdminControlPanelUsersViewModel()
                {
                    Id = applicationUser.Id,
                    UserName = applicationUser.UserName,
                    Email = applicationUser.Email,
                    TaskAmount = applicationUser.UserTasks.Count,
                    AttemptAmount = applicationUser.AttemptAmount,
                    SolutionAmount = applicationUser.UserSolvedTask.Count,
                    UserRate = String.Format("{0:0.0#}", applicationUser.UserRate),
                    Delete = false,
                    IsAdmin = UserManager.IsInRole(applicationUser.Id, "admin"),
                    LockoutEndDateUtc = applicationUser.LockoutEndDateUtc != null ? applicationUser.LockoutEndDateUtc.Value.Date.ToString("d", CultureInfo.CreateSpecificCulture("en-Us")) : ""
                };
                users.Add(user);
            }
            ViewBag.TotalUsers = UserManager.Users.Count();
            ViewBag.SortOrder = !sortOrder;
            return View(Helpers.Helpers.UsersSort(users, sortParam, sortOrder));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AdminControlPanelUsers(IList<AdminControlPanelUsersViewModel> users)
        {
            foreach (var user in users)
            {
                bool isUserChanged = false;
                var applicationUser = UserManager.FindById(user.Id);
                if (user.Delete)
                {
                    UserManager.Delete(applicationUser);
                }
                else
                {
                    if (Helpers.Helpers.UserLockoutChange(applicationUser, user.LockoutEndDateUtc))
                    {
                        isUserChanged = true;
                    };
                    if ((UserManager.IsInRole(applicationUser.Id, "admin") != user.IsAdmin))
                    {
                        if (user.IsAdmin)
                        {
                            UserManager.AddToRole(applicationUser.Id, "admin");
                        }
                        else
                        {
                            UserManager.RemoveFromRole(applicationUser.Id, "admin");
                        }
                        isUserChanged = true;
                    }
                    if (isUserChanged)
                    {
                        UserManager.Update(applicationUser);
                    }
                }
            }
            return RedirectToAction("AdminControlPanelUsers");
        }

        [Authorize(Roles = "admin")]
        public ActionResult AdminControlPanelTasks(string userId, string sortParam, bool? sortOrder = true)
        {
            var userTasks = userTaskRepository.GetAll();
            if (userId != null)
            {
                userTasks = userTaskRepository.GetUserTasks(userId);
                var applicationUser = UserManager.FindById(userId);
                ViewBag.UserName = applicationUser.UserName;
                ViewBag.UserId = applicationUser.Id;

            }
            var tasks = new List<AdminControlPanelTasksViewModel>();
            foreach (var userTask in userTasks)
            {
                var task = new AdminControlPanelTasksViewModel()
                {
                    Id = userTask.Id,
                    TaskTitle = userTask.UserTaskTitle,
                    Category = categoryRepository.GetById(userTask.CategoryId).CategoryName,
                    CommentAmount = userTask.Comments.Count,
                    SolutionAmount = userTask.SolvedTasks.Count,
                    LikeAmount = userTask.Likes.Count,
                    DateAdded = userTask.DateAdded.ToString("d", CultureInfo.CreateSpecificCulture("en-US")),
                    TaskStatus = userTask.UserTaskStatus,
                    Delete = false,
                    UserId = userTask.User.Id,
                    UserName = UserManager.FindById(userTask.User.Id).UserName
                };
                tasks.Add(task);
            }
            ViewBag.TotalTasks = userTasks.Count;
            ViewBag.SortOrder = !sortOrder;
            return View(Helpers.Helpers.TasksSort(tasks, sortParam, sortOrder));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AdminControlPanelTasks(IList<AdminControlPanelTasksViewModel> tasks, string userId)
        {
            foreach (var task in tasks)
            {
                bool isTaskChanged = false;
                var userTask = userTaskRepository.GetById(task.Id);
                if (task.Delete)
                {
                    userTaskRepository.Delete(task.Id);
                }
                else
                {
                    if (userTask.UserTaskStatus != task.TaskStatus)
                    {
                        userTask.UserTaskStatus = task.TaskStatus;
                        isTaskChanged = true;
                    }
                    if (isTaskChanged)
                    {
                        userTaskRepository.Update(userTask);
                    }
                }
            }
            return RedirectToAction("AdminControlPanelTasks", userId);
        }
    }
}