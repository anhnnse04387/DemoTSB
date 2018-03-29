using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.HangHoa.Controllers
{
    public class ErrorPageController : Controller
    {
        // GET: HangHoa/ErrorPage
        public ActionResult Index()
        {
            ViewBag.ErrorString = "Trang này đang được xây dựng";
            return View();
        }
    }
}