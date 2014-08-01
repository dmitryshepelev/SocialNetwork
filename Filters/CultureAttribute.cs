using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Filters
{
    public class CulturesList
    {
        private List<string> cultures = new List<string>
        {
            "en",
            "ru"
        };

        public List<string> GetCulturesList()
        {
            return this.cultures;
        }

        public void AddCulturesToList(List<string> cultures)
        {
            foreach (string culture in cultures)
            {
                this.cultures.Add(culture);
            }
        }

        public string CheckCurrentCulture(string cultureLanguage)
        {
            if( !cultures.Contains(cultureLanguage) )
            {
                return "en";
            }
            return cultureLanguage;
        }
    }

    public class CultureAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string cultureName = null;
            HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies["language"];
            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value;
            }
            else
            {
                cultureName = "en";
            }
            CulturesList culture = new CulturesList();
            cultureName = culture.CheckCurrentCulture(cultureName);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}