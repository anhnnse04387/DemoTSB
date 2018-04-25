﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.KeToan.Controllers
{
    public class KeToanBaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", Area = "" }));
            }
            else
            {
                switch (session.roleSelectedID)
                {
                    case 1:
                        filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home", action = "Index", Area = "QuanTri" }));
                        break;
                    case 3:
                        filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home", action = "Index", Area = "PhanPhoi" }));
                        break;
                    case 4:
                        filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home", action = "Index", Area = "HangHoa" }));
                        break;
                    case 2:
                        filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home", action = "Index", Area = "QuanLy" }));
                        break;
                    default:
                        break;
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}