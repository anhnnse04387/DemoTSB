using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.QuanLy.Models;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers
{
    public class PreviewController : Controller
    {
        // GET: PhanPhoi/Preview
        public ActionResult Index(OrderTotalModel model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult CheckOut(OrderTotalModel model)
        {
            return RedirectToAction("View");
        }
    }
}