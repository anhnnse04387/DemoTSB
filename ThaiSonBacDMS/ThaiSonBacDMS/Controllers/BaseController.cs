using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            if(session!=null)
            {
                switch (session.roleSelectedID)
                {
                    case 1:
                        filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home", action = "Index", Area = "QuanTri" }));
                        break;
                    case 2:
                        filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home", action = "Index", Area = "QuanLy" }));
                        break;
                    case 3:
                        filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home", action = "Index", Area = "PhanPhoi" }));
                        break;
                    case 4:
                        filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home", action = "Index", Area = "HangHoa" }));
                        break;
                    case 5:
                        filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home", action = "Index", Area = "KeToan" }));
                        break;
                    default:
                        break;
                }
            }else
            {
                RedirectToAction("Index", "Login");
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}