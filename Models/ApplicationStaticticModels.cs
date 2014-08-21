using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class MostSolvedTaskViewModel
    {
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public int SolutionsAmount { get; set; }
    }

    public class RecentTaskViewModel
    {
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public DateTime DateAdded { get; set; }
    }

    public class TopRateUsersViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public double Rate { get; set; }
    }
}