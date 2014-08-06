using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace SocialNetwork.Models
{
    [Table("Categories")]
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<UserTaskModel> UserTasks { get; set; }
    }
}