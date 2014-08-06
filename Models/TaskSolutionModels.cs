using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    [Table("TaskSolutions")]
    public class TaskSolutionModel
    {
        [Key]
        public int Id { get; set; }
        public string Solution { get; set; }
        public int UserTaskId { get; set; }
        public virtual UserTaskModel UserTask { get; set; }
    }
}