using System;
using System.Collections.Generic;
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
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            CulturesList culture = new CulturesList();
            language = culture.CheckCurrentCulture(language);
            HttpCookie cookie = Request.Cookies["language"];
            if (cookie != null)
            {
                cookie.Value = language;
            }
            else
            {

                cookie = new HttpCookie("language");
                cookie.HttpOnly = false;
                cookie.Value = language;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }
    }
}