using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialNetwork.Models;

namespace SocialNetwork.Helpers
{
    public static class Helpers
    {
        public static ImageUploadResult UploadImage(HttpPostedFileBase imageToUpload)
        {
            ImageUploadParams uploadParams = new ImageUploadParams();
            uploadParams.File = new FileDescription(imageToUpload.FileName, imageToUpload.InputStream);
            CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(new Account("slideshowapp", "738528734478378",
            "Yytgqtd5iklPE9L23nmH0xskUxw"));
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            return uploadResult;
        }

        public static string TransformImage(string url, int size)
        {
            var index = url.IndexOf("upload/") + 7;
            var firstPath = url.Substring(0, index);
            var secondPath = url.Substring(index);
            return String.Format("{0}c_thumb,h_{2},w_{2}/{1}", firstPath, secondPath, size);
        }

        public static string TransformImage(string url, int size, int width)
        {
            string result = "";
            var index = url.IndexOf("upload/") + 7;
            var firstPath = url.Substring(0, index);
            var secondPath = url.Substring(index);
            if (width < 522)
            {
                size = width;
            }
            return String.Format("{0}c_scale,w_{2}/{1}", firstPath, secondPath, size);
        }

        public static DateTime? ConvertFromString(string str)
        {
            DateTime? result = null;
            if (str != null)
            {
                string[] dateDivide = str.Split('/');
                string newDateString = String.Format("{0}/{1}/{2}", dateDivide[1], dateDivide[0], dateDivide[2]);
                result = DateTime.Parse(newDateString);
            }
            return result;
        }

        public static string StringConventor(string str)
        {
            return String.Format("{0}...", str.Substring(0, Math.Min(str.Length, 200)));
        }

        public static bool UserLockoutChange(IdentityUser applicationUser, string lockoutEndDateString)
        {
            bool isLockoutEndDateChanged = false;
            DateTime? lockoutEndDate = ConvertFromString(lockoutEndDateString);
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

        public static string CreateVideo(string str)
        {
            var videoTagStart = "[VIDEO]";
            var videoTagEnd = "[/VIDEO]";
            while (str.Contains(videoTagStart) || str.Contains(videoTagEnd))
            {
                str = str.Replace(videoTagStart, "");
                str = str.Replace(videoTagEnd, "");
            }
            return str;
        }

        public static string CreateImage(string str)
        {
            var imageTagStart = "[IMAGE]";
            var imageTagEnd = "[/IMAGE]";
            while (str.Contains(imageTagStart) || str.Contains(imageTagEnd))
            {
                str = str.Replace(imageTagStart, "");
                str = str.Replace(imageTagEnd, "");
            }
            return str;
        }

        public static string CreateEquation(string str)
        {
            var funcRegStart = "[FUNC]";
            var funcRegEnd = "[/FUNC]";
            var equationTemplate = "![ecuatoin](http://latex.codecogs.com/gif.latex?";
            while (str.Contains(funcRegStart))
            {
                var indexStart = str.IndexOf(funcRegStart);
                var indexEnd = str.IndexOf(funcRegEnd);
                var equation = HttpUtility.UrlEncode(str.Substring(indexStart + 6, indexEnd - indexStart - 6));
                str = str.Replace(str.Substring(indexStart, indexEnd + 6),
                    String.Format("{0}{1})", equationTemplate, equation));
            }
            return str;
        }

        public static List<AdminControlPanelUsersViewModel> UsersSort(List<AdminControlPanelUsersViewModel> users,
            string sortParam, bool? sortOrder)
        {
            List<AdminControlPanelUsersViewModel> sortedUsers;
            switch (sortParam)
            {
                case "Email":
                    sortedUsers = (sortOrder == true || sortOrder == null)
                        ? users.OrderBy(x => x.Email).ToList()
                        : users.OrderByDescending(x => x.Email).ToList();
                    break;
                case "TaskAmount":
                    sortedUsers = (sortOrder == true || sortOrder == null)
                        ? users.OrderBy(x => x.TaskAmount).ToList()
                        : users.OrderByDescending(x => x.TaskAmount).ToList();
                    break;
                case "AttemptAmount":
                    sortedUsers = (sortOrder == true || sortOrder == null)
                        ? users.OrderBy(x => x.AttemptAmount).ToList()
                        : users.OrderByDescending(x => x.AttemptAmount).ToList();
                    break;
                case "SolutionAmount":
                    sortedUsers = (sortOrder == true || sortOrder == null)
                        ? users.OrderBy(x => x.SolutionAmount).ToList()
                        : users.OrderByDescending(x => x.SolutionAmount).ToList();
                    break;
                case "UserRate":
                    sortedUsers = (sortOrder == true || sortOrder == null)
                        ? users.OrderBy(x => x.UserRate).ToList()
                        : users.OrderByDescending(x => x.UserRate).ToList();
                    break;
                case "LockoutEndDateUtc":
                    sortedUsers = (sortOrder == true || sortOrder == null)
                        ? users.OrderBy(x => x.LockoutEndDateUtc).ToList()
                        : users.OrderByDescending(x => x.LockoutEndDateUtc).ToList();
                    break;
                case "IsAdmin":
                    sortedUsers = (sortOrder == true || sortOrder == null)
                        ? users.OrderBy(x => x.IsAdmin).ToList()
                        : users.OrderByDescending(x => x.IsAdmin).ToList();
                    break;
                default:
                    sortedUsers = (sortOrder == true || sortOrder == null)
                        ? users.OrderBy(x => x.UserName).ToList()
                        : users.OrderByDescending(x => x.UserName).ToList();
                    break;
            }
            return sortedUsers;
        }

        public static List<AdminControlPanelTasksViewModel> TasksSort(List<AdminControlPanelTasksViewModel> tasks,
            string sortParam, bool? sortOrder)
        {
            List<AdminControlPanelTasksViewModel> sortedTasks;
            switch (sortParam)
            {
                case "Category":
                    sortedTasks = (sortOrder == true || sortOrder == null)
                        ? tasks.OrderBy(x => x.Category).ToList()
                        : tasks.OrderByDescending(x => x.Category).ToList();
                    break;
                case "CommentAmount":
                    sortedTasks = (sortOrder == true || sortOrder == null)
                        ? tasks.OrderBy(x => x.CommentAmount).ToList()
                        : tasks.OrderByDescending(x => x.CommentAmount).ToList();
                    break;
                case "TaskStatus":
                    sortedTasks = (sortOrder == true || sortOrder == null)
                        ? tasks.OrderBy(x => x.TaskStatus).ToList()
                        : tasks.OrderByDescending(x => x.TaskStatus).ToList();
                    break;
                case "SolutionAmount":
                    sortedTasks = (sortOrder == true || sortOrder == null)
                        ? tasks.OrderBy(x => x.SolutionAmount).ToList()
                        : tasks.OrderByDescending(x => x.SolutionAmount).ToList();
                    break;
                case "DateAdded":
                    sortedTasks = (sortOrder == true || sortOrder == null)
                        ? tasks.OrderBy(x => x.DateAdded).ToList()
                        : tasks.OrderByDescending(x => x.DateAdded).ToList();
                    break;
                case "LikeAmount":
                    sortedTasks = (sortOrder == true || sortOrder == null)
                        ? tasks.OrderBy(x => x.LikeAmount).ToList()
                        : tasks.OrderByDescending(x => x.LikeAmount).ToList();
                    break;
                case "UserName":
                    sortedTasks = (sortOrder == true || sortOrder == null)
                        ? tasks.OrderBy(x => x.UserName).ToList()
                        : tasks.OrderByDescending(x => x.UserName).ToList();
                    break;
                default:
                    sortedTasks = (sortOrder == true || sortOrder == null)
                        ? tasks.OrderBy(x => x.TaskTitle).ToList()
                        : tasks.OrderByDescending(x => x.TaskTitle).ToList();
                    break;
            }
            return sortedTasks;
        }

        public static List<UserTaskModel> TasksSort(List<UserTaskModel> tasks,
     string sortParam, bool? sortOrder)
        {
            List<UserTaskModel> sortedTasks;
            switch (sortParam)
            {
                case "Category":
                    sortedTasks = (sortOrder == true || sortOrder == null)
                        ? tasks.OrderBy(x => x.Category).ToList()
                        : tasks.OrderByDescending(x => x.Category).ToList();
                    break;
                case "CommentAmount":
                    sortedTasks = (sortOrder == true || sortOrder == null)
                        ? tasks.OrderBy(x => x.Comments.Count).ToList()
                        : tasks.OrderByDescending(x => x.Comments.Count).ToList();
                    break;
                case "SolutionAmount":
                    sortedTasks = (sortOrder == true || sortOrder == null)
                        ? tasks.OrderBy(x => x.SolvedTasks.Count).ToList()
                        : tasks.OrderByDescending(x => x.SolvedTasks.Count).ToList();
                    break;
                case "LikeAmount":
                    sortedTasks = (sortOrder == true || sortOrder == null)
                        ? tasks.OrderBy(x => x.Likes.Count).ToList()
                        : tasks.OrderByDescending(x => x.Likes.Count).ToList();
                    break;
                default:
                    sortedTasks = (sortOrder == true || sortOrder == null)
                        ? tasks.OrderBy(x => x.DateAdded).ToList()
                        : tasks.OrderByDescending(x => x.DateAdded).ToList();
                    break;
            }
            return sortedTasks;
        }
    }
}