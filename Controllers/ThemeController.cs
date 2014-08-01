using System;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Filters;

namespace SocialNetwork.Controllers
{
    public class ThemeController : Controller
    {
        // GET: Theme
        public ActionResult ChangeTheme(string theme)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            ThemesList themeList = new ThemesList();
            theme = themeList.CheckCurrentTheme(theme);
            HttpCookie cookie = Request.Cookies["theme"];
            if (cookie != null)
            {
                cookie.Value = theme;
            }
            else
            {
                cookie = new HttpCookie("theme")
                {
                    HttpOnly = false,
                    Value = theme,
                    Expires = DateTime.Now.AddYears(1)
                };
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }
    }
}