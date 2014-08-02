using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class TagModel
    {
        public int Id { get; set; }
        [Display(Name = "TagsName", ResourceType = typeof(Resources.Resource))]
        public string TagName { get; set; }
    }
}