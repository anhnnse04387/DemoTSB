using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Controllers;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers
{
    public class HomeController : BaseController
    {
        // GET: QuanLy/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}