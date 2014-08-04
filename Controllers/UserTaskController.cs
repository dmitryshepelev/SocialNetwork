using System.Web.Mvc;
using SocialNetwork.Models;
using SocialNetwork.Repository;

namespace SocialNetwork.Controllers
{
    public class UserTaskController : Controller
    {
        // GET: UserTask
        private Repositories repositories = new Repositories();

        public ActionResult Index()
        {
            return View();
        }
    }
}