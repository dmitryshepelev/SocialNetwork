using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class SolutionModel
    {
        public int Id { get; set; }
        [Display(Name = "Solution", ResourceType = typeof(Resources.Resource))]
        public string Solution { get; set; }
        public int UserTaskId { get; set; }
        public virtual UserTaskModel UserTask { get; set; }
    }
}