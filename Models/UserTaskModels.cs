using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual ICollection<UserTaskTagModel> Tags { get; set; }
        public virtual ICollection<CommentModel> Comments { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }

    public class UserTasksViewAllModel
    {
        public int Id { get; set; }
        public string UserTaskTitle { get; set; }
        public bool UserTaskStatus { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int LikesAmount { get; set; }
        public int SolutionsAmount { get; set; }
        public List<TagModel> Tags { get; set; }
        public int CommentsAmount { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
    }

    public class UserTasksViewModel
    {
        public int Id { get; set; }
        public string UserTaskTitle { get; set; }
        public bool UserTaskStatus { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public int LikesAmount { get; set; }
        public int SolutionsAmount { get; set; }
        public int CommentsAmount { get; set; }
        public List<TagModel> Tags { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public bool IsSolved { get; set; }
    }

    public class CreateTaskViewModel
    {
        public string UserTaskTitle { get; set; }
        public string Tags { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public string Answers { get; set; }
    }
}