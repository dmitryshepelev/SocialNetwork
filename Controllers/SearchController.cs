using System;
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

namespace SocialNetwork.Controllers
{
    public class SearchController : Controller
    {

        // GET: Search
        public ActionResult SearchResult(string searchingString)
        {
            if (string.IsNullOrEmpty(searchingString))
            {
                return RedirectToAction("ViewAllTasks", "UserTask");
            }
            var searchUsingLucene = new LuceneSearch();
          //  searchUsingLucene.BuildIndex();
            var foundIds = searchUsingLucene.SearchResult(searchingString);
            var ac = new ApplicationDbContext();
            
            //var foundUsers = foundIds["Users"].Select(t=>ac.U)
            var foundElements = foundIds.Select(t => ac.UserTasks.Find(t)).ToList();
            return View(foundElements);
        }
    }
}