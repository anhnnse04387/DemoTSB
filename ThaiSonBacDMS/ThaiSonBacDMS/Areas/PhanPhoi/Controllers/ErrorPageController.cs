using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Controllers;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class ErrorPageController : BaseController
    {
        // GET: PhanPhoi/ErrorPage
        public ActionResult Index()
        {
            ViewBag.ErrorString = "Trang này đang được xây dựng";
            return View();
        }
    }
}