using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace SocialNetwork.Filters
{
    public class ThemesList
    {
        private List<string> themes = new List<string>
        {
            "Light",
            "Dark"
        };

        public List<string> GetThemesList()
        {
            return this.themes;
        }

        public void AddThemesToList(List<string> themes)
        {
            foreach (string theme in themes)
            {
                this.themes.Add(theme);
            }
        }

        public string CheckCurrentTheme(string theme)
        {
            if (!themes.Contains(theme))
            {
                return "Light";
            }
            return theme;
        }
    }
    public class ThemesAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string themeName = null;
            HttpCookie themeCookie = filterContext.HttpContext.Request.Cookies["theme"];
            if (themeCookie != null)
            {
                themeName = themeCookie.Value;
            }
            else
            {
                themeName = "Light";
            }
            ThemesList theme = new ThemesList();
            themeName = theme.CheckCurrentTheme(themeName);
            if (themeName == "Light") Scripts.Render("~/bundles/cssLight");
            else Scripts.Render("~/bundles/cssDark");
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}