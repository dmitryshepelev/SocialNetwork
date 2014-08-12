using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialNetwork.Models;

namespace SocialNetwork.Helpers
{
    public static class AdminManagement
    {
        public static bool UserLockoutChange(IdentityUser applicationUser, string lockoutEndDateString)
        {
            bool isLockoutEndDateChanged = false;
            DateTime? lockoutEndDate = Conventors.ConvertFromString(lockoutEndDateString);
            if (applicationUser.LockoutEndDateUtc != lockoutEndDate)
            {
                if (lockoutEndDate == null)
                {
                    applicationUser.LockoutEndDateUtc = null;
                    applicationUser.LockoutEnabled = false;
                }
                else
                {
                    applicationUser.LockoutEndDateUtc = lockoutEndDate;
                    applicationUser.LockoutEnabled = true;
                }
                isLockoutEndDateChanged = true;
            }
            return isLockoutEndDateChanged;
        }

        public static List<AdminControlPanelUsersViewModel> UsersSort(List<AdminControlPanelUsersViewModel> users, string sortParam, bool? sortOrder)
        {
            List<AdminControlPanelUsersViewModel> sortedUsers;
            switch (sortParam)
            {
                case "Email":
                    sortedUsers = (sortOrder == true || sortOrder == null) ? users.OrderBy(x => x.Email).ToList() : users.OrderByDescending(x => x.Email).ToList();
                    break;
                case "TaskAmount":
                    sortedUsers = (sortOrder == true || sortOrder == null) ? users.OrderBy(x => x.TaskAmount).ToList() : users.OrderByDescending(x => x.TaskAmount).ToList();
                    break;
                case "AttemptAmount":
                    sortedUsers = (sortOrder == true || sortOrder == null) ? users.OrderBy(x => x.AttemptAmount).ToList() : users.OrderByDescending(x => x.AttemptAmount).ToList();
                    break;
                case "SolutionAmount":
                    sortedUsers = (sortOrder == true || sortOrder == null) ? users.OrderBy(x => x.SolutionAmount).ToList() : users.OrderByDescending(x => x.SolutionAmount).ToList();
                    break;
                case "UserRate":
                    sortedUsers = (sortOrder == true || sortOrder == null) ? users.OrderBy(x => x.UserRate).ToList() : users.OrderByDescending(x => x.UserRate).ToList();
                    break;
                case "LockoutEndDateUtc":
                    sortedUsers = (sortOrder == true || sortOrder == null) ? users.OrderBy(x => x.LockoutEndDateUtc).ToList() : users.OrderByDescending(x => x.LockoutEndDateUtc).ToList();
                    break;
                case "IsAdmin":
                    sortedUsers = (sortOrder == true || sortOrder == null) ? users.OrderBy(x => x.IsAdmin).ToList() : users.OrderByDescending(x => x.IsAdmin).ToList();
                    break;
                default:
                    sortedUsers = (sortOrder == true || sortOrder == null) ? users.OrderBy(x => x.UserName).ToList() : users.OrderByDescending(x => x.UserName).ToList();
                    break;
            }
            return sortedUsers;
        }

        public static List<AdminControlPanelTasksViewModel> TasksSort(List<AdminControlPanelTasksViewModel> tasks, string sortParam, bool? sortOrder)
        {
            List<AdminControlPanelTasksViewModel> sortedTasks;
            switch (sortParam)
            {
                case "Category":
                    sortedTasks = (sortOrder == true || sortOrder == null) ? tasks.OrderBy(x => x.Category).ToList() : tasks.OrderByDescending(x => x.Category).ToList();
                    break;
                case "CommentAmount":
                    sortedTasks = (sortOrder == true || sortOrder == null) ? tasks.OrderBy(x => x.CommentAmount).ToList() : tasks.OrderByDescending(x => x.CommentAmount).ToList();
                    break;
                case "TaskStatus":
                    sortedTasks = (sortOrder == true || sortOrder == null) ? tasks.OrderBy(x => x.TaskStatus).ToList() : tasks.OrderByDescending(x => x.TaskStatus).ToList();
                    break;
                case "SolutionAmount":
                    sortedTasks = (sortOrder == true || sortOrder == null) ? tasks.OrderBy(x => x.SolutionAmount).ToList() : tasks.OrderByDescending(x => x.SolutionAmount).ToList();
                    break;
                case "DateAdded":
                    sortedTasks = (sortOrder == true || sortOrder == null) ? tasks.OrderBy(x => x.DateAdded).ToList() : tasks.OrderByDescending(x => x.DateAdded).ToList();
                    break;
                case "LikeAmount":
                    sortedTasks = (sortOrder == true || sortOrder == null) ? tasks.OrderBy(x => x.LikeAmount).ToList() : tasks.OrderByDescending(x => x.LikeAmount).ToList();
                    break;
                case "UserName":
                    sortedTasks = (sortOrder == true || sortOrder == null) ? tasks.OrderBy(x => x.UserName).ToList() : tasks.OrderByDescending(x => x.UserName).ToList();
                    break;
                default:
                    sortedTasks = (sortOrder == true || sortOrder == null) ? tasks.OrderBy(x => x.TaskTitle).ToList() : tasks.OrderByDescending(x => x.TaskTitle).ToList();
                    break;
            }
            return sortedTasks;
        }
    }
}