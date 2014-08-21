using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class SearchResultModels
    {
        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<UserTaskModel> Tasks { get; set; }

        public SearchResultModels()
        {
            Users = new Collection<ApplicationUser>();
            Tasks = new Collection<UserTaskModel>();
        }
    }
}