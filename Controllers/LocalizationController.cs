using System;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Filters;

namespace SocialNetwork.Controllers
{
    [Culture]
    public class LocalizationController : Controller
    {
        // GET: Localization
        public ActionResult ChangeCulture(string language)
        {
            if (Request.UrlReferrer == null)
            {
                return HttpNotFound();
            }
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            var culture = new CulturesList();
            language = culture.CheckCurrentCulture(language);
            HttpCookie cookie = Request.Cookies["language"];
            if (cookie != null)
            {
                cookie.Value = language;
            }
            else
            {
                cookie = new HttpCookie("language")
                {
                    HttpOnly = false,
                    Value = language,
                    Expires = DateTime.Now.AddYears(1)
                };
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }
    }
}