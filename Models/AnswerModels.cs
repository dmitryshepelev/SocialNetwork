using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }
        [Display(Name = "Answers", ResourceType = typeof(Resources.Resource))]
        public string Answer { get; set; }
    }
}