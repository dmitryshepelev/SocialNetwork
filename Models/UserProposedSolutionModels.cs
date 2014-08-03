using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class UserProposedSolutionModel
    {
        public int Id { get; set; }
        [Display(Name = "UserProposedSolution", ResourceType = typeof(Resources.Resource))]
        public string ProposedSolution { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int UserTaskId { get; set; }
        public virtual UserTaskModel UserTask { get; set; }
    }
}