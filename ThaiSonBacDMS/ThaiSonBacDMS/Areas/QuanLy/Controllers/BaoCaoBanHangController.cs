using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers
{
    public class BaoCaoBanHangController : QuanLyBaseController
    {
        // GET: QuanLy/BaoCaoBanHang
        public ActionResult Index()
        {
            return View();
        }
    }
}