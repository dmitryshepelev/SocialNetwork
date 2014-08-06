using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    [Table("Comments")]
    public class CommentModel
    {
        [Key]
        public int Id { get; set; }
        public string CommentContent { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int UserTaskId { get; set; }
        public virtual UserTaskModel UserTask { get; set; }
    }
}