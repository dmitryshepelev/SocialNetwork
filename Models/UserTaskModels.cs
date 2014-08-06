using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace SocialNetwork.Models
{
    [Table("UserTasks")]
    public class UserTaskModel
    {
        [Key]
        public int Id { get; set; }
        public string UserTaskTitle { get; set; }
        public string UserTaskContent { get; set; }
        public bool UserTaskStatus { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<LikeModel> Likes { get; set; }
        public virtual ICollection<UserSolvedTaskModel> SolvedTasks { get; set; }
        public virtual ICollection<TaskSolutionModel> Answers { get; set; }
        public virtual ICollection<TagModel> Tags { get; set; }
        public virtual ICollection<CommentModel> Comments { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}