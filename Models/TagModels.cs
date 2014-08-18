using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    [Table("Tags")]
    public class TagModel
    {
        [Key]
        public int Id { get; set; }
        public string TagName { get; set; }
        public virtual ICollection<UserTaskTagModel> Tasks { get; set; }
    }
}