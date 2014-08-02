using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace SocialNetwork.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Display(Name = "CategoryName", ResourceType = typeof(Resources.Resource))]
        public string CategoryName { get; set; }
    }
}