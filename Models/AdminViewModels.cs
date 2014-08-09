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
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
    }

    public class AdminControlPanelTasksViewModel
    {
        public int Id { get; set; }
        public string TaskTitle { get; set; }
        public int CommentAmount { get; set; }
        public bool TaskStatus { get; set; }
        public DateTime DateAdded { get; set; }
        public int SolutionAmount { get; set; }
        public int LikeAmount { get; set; }
    }
}