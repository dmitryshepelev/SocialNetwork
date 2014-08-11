using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SocialNetwork.Models
{
    public class AdminControlPanelUsersViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int TaskAmount { get; set; }
        public int AttemptAmount { get; set; }
        public int SolutionAmount { get; set; }
        public double UserRate { get; set; }
        public bool Delete { get; set; }
        public string LockoutEndDateUtc { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class AdminControlPanelTasksViewModel
    {
        public int Id { get; set; }
        public string TaskTitle { get; set; }
        public string Category { get; set; }
        public int CommentAmount { get; set; }
        public bool TaskStatus { get; set; }
        public string DateAdded { get; set; }
        public int SolutionAmount { get; set; }
        public int LikeAmount { get; set; }
        public bool Delete { get; set; }
    }
}