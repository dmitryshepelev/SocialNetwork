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
using SocialNetwork.Models;
using SocialNetwork.SearchLucene;

namespace SocialNetwork.Controllers
{
    public class SearchController : Controller
    {

        // GET: Search
        public ActionResult SearchResult(string searchingString)
        {
            var ac = new ApplicationDbContext();
            if (string.IsNullOrEmpty(searchingString))
            {
                return RedirectToAction("ViewAllTasks", "UserTask");
            }
            var searchUsingLucene = new LuceneSearch();
            var foundIds = searchUsingLucene.SearchResult(searchingString);
            var searchModel = new SearchResultModels();
            foreach (var IdFoundElement in foundIds)
            {
                searchModel.Tasks.Add(ac.UserTasks.Find(IdFoundElement));
            }
            //foreach (var IdFoundElement in foundIds["User"])
            //{
            //    searchModel.Users.Add(ac.UserTasks.Find(IdFoundElement).User);
            //}
            var searchUsers = new SearchUserLucene();
            var foundUserIds = searchUsers.SearchResult(searchingString);
            foreach (var IdFoundElement in foundUserIds)
            {
                searchModel.Users.Add(ac.Users.Find(IdFoundElement));
            }
            return View(searchModel);
        }
    }
}