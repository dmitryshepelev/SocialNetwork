using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace SocialNetwork.Models
{
    public class UserTaskModel
    {
        public int Id { get; set; }

        [Display(Name = "UserTaskTitle", ResourceType = typeof(Resources.Resource))]
        public string UserTaskTitle { get; set; }
        [Display(Name = "UserTaskContent", ResourceType = typeof(Resources.Resource))]
        public string UserTaskContent { get; set; }
        [Display(Name = "UserTaskStatus", ResourceType = typeof(Resources.Resource))]
        public bool UserTaskStatus { get; set; }
        public DateTime DateAdded { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<LikeModel> Likes { get; set; }
        public virtual ICollection<UserProposedSolutionModel> SolvedTasks { get; set; }
        public virtual ICollection<SolutionModel> Answers { get; set; }
        public virtual ICollection<TagModel> Tags { get; set; }
        public virtual ICollection<CommentModel> Comments { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}