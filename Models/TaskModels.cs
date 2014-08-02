using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace SocialNetwork.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Display(Name = "TaskTitle", ResourceType = typeof(Resources.Resource))]
        public string TaskTitle { get; set; }
        [Display(Name = "TaskContent", ResourceType = typeof(Resources.Resource))]
        public string TaskContent { get; set; }
        [Display(Name = "TaskStatus", ResourceType = typeof(Resources.Resource))]
        public bool TaskStatus { get; set; }
        public DateTime DateAdded { get; set; }
    }
}