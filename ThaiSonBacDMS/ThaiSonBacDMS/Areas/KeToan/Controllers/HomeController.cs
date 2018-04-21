using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Controllers;

namespace ThaiSonBacDMS.Areas.KeToan.Controllers
{
    public class HomeController : KeToanBaseController
    {
        // GET: KeToan/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}