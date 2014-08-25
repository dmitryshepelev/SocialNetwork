using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SocialNetwork.Filters;
using SocialNetwork.Models;
using SocialNetwork.Repository;
using SocialNetwork.Repository.Implementations;
using SocialNetwork.Repository.Interfaces;
using SocialNetwork.SearchLucene;

namespace SocialNetwork.Controllers
{
    [Culture]
    public class SearchController : Controller
    {
        private IUserTaskRepository userTaskRepository;
        private ApplicationUserManager userManager ;
        public SearchController()
        {
        }

        public SearchController(IUserTaskRepository userTaskRepository)
        {
            this.userTaskRepository = userTaskRepository;
        }

         public SearchController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }
        // GET: Search
        public ActionResult SearchResult(string searchingString)
        {
            if (string.IsNullOrWhiteSpace(searchingString))
            {
                return RedirectToAction("ViewAllTasks", "UserTask");
            }
            var searchUsingLucene = new LuceneSearch();
            var foundIds = searchUsingLucene.SearchResult(searchingString);
            var searchModel = new SearchResultModels();
            foreach (var IdFoundElement in foundIds)
            {
                searchModel.Tasks.Add(userTaskRepository.GetById(IdFoundElement));
            }
            var searchUsers = new SearchUserLucene();
            var foundUserIds = searchUsers.SearchResult(searchingString);
            foreach (var IdFoundElement in foundUserIds)
            {
                searchModel.Users.Add(UserManager.FindById(IdFoundElement));
            }
            return View(searchModel);
        }
    }
}