using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    [Table("UserTaskTags")]
    public class UserTaskTagModel
    {
        [Key]
        public int Id { get; set; }
        public int TagId { get; set; }
        public TagModel Tag { get; set; }
        public int UserTaskId { get; set; }
        public UserTaskModel UserTask { get; set; }
    }
}