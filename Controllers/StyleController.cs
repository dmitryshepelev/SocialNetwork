using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers
{
    public class StyleController : Controller
    {
        // GET: Style
        public ActionResult ChangeTheme(string theme)
        {
            if (Request.UrlReferrer == null)
            {
                return HttpNotFound();
            }
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            HttpCookie cookie = new HttpCookie("theme")
            {
                HttpOnly = false,
                Value = theme
            };
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }
    }
}