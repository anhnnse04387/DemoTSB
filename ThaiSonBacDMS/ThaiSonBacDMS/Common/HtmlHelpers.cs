using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.HtmlHelpers
{
    public static class HtmlHelpers
    {
        public static string IsActive(this HtmlHelper html, string action, string control)
        {
            var routeData = html.ViewContext.RouteData;
            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];
            // both must match
            var returnActive = control == routeControl && action == routeAction;
            return returnActive ? "active" : "";
        }
    }
}