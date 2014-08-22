using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    [Table("Charts")]
    public class ChartModel
    {
        [Key]
        public int Id { get; set; }
        public string Expression { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Step { get; set; }
        public string AxisXName { get; set; }
        public string AxisYName { get; set; }
        public string ChartName { get; set; }
        public int UserTaskId { get; set; }
        public UserTaskModel UserTask { get; set; }
    }
}