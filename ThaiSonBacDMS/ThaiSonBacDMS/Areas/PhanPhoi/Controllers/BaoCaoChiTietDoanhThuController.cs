using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;
using ThaiSonBacDMS.Controllers;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class BaoCaoChiTietDoanhThuController : BaseController
    {
        // GET: PhanPhoi/BaoCaoChiTietDoanhThu
        [HttpGet]
        public ActionResult Index()
        {
            ChiTietBaoCaoDoanhThu model = new ChiTietBaoCaoDoanhThu();
            //set year in db
            model.listShowYear = new List<SelectListItem>();
            foreach (var i in new OrderTotalDAO().getListYear())
            {
                var item = new SelectListItem { Text = i.ToString(), Value = i.ToString() };
                model.listShowYear.Add(item);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ChiTietBaoCaoDoanhThu model)
        {
            int numberFrom = 0;
            int numberTo = 0;
            decimal priceFrom = 0;
            decimal priceTo = 0;
            decimal doanhThuFrom = 0;
            decimal doanhThuTo = 0;
            //set year in db
            model.listShowYear = new List<SelectListItem>();
            foreach (var i in new OrderTotalDAO().getListYear())
            {
                var item = new SelectListItem { Text = i.ToString(), Value = i.ToString() };
                model.listShowYear.Add(item);
            }
            try
            {
                numberFrom = model.numberSoldFrom;
                numberTo = model.numberSoldTo;
                priceFrom = model.priceFrom;
                priceTo = model.priceTo;
                doanhThuFrom = model.doanhThuFrom;
                model.doanhThuTo = model.doanhThuTo;
                if (numberFrom > numberTo)
                {
                    throw new Exception("Số lượng từ nhỏ hơn giá đến");
                }
                if (priceFrom > priceTo)
                {
                    throw new Exception("Già từ nhỏ hơn giá đến");
                }

            }
            catch(Exception e)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}