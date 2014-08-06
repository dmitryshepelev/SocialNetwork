using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    [Table("UserSolvedTasks")]
    public class UserSolvedTaskModel
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int UserTaskId { get; set; }
        public virtual UserTaskModel UserTask { get; set; }
    }
}