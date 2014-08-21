using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class TagCloudModel
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public int TagFrequency { get; set; }
    }
}