using Models.DAO;
using Models.DAO_Model;
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
            int? productID = null;
            int? numberFrom = null;
            int? numberTo = null;
            decimal? priceFrom = null;
            decimal? priceTo = null;
            decimal? doanhThuFrom = null;
            decimal? doanhThuTo = null;
            List<DataChiTietDoanhThu> data = new List<DataChiTietDoanhThu>();
            //set year in db
            model.listShowYear = new List<SelectListItem>();
            foreach (var i in new OrderTotalDAO().getListYear())
            {
                var item = new SelectListItem { Text = i.ToString(), Value = i.ToString() };
                model.listShowYear.Add(item);
            }
            try
            {
                if(!string.IsNullOrEmpty(model.productID))
                {
                    productID = int.Parse(model.productID);
                }
                if (!string.IsNullOrEmpty(model.numberSoldFrom))
                {
                    numberFrom = int.Parse(model.numberSoldFrom);
                }
                if (!string.IsNullOrEmpty(model.numberSoldTo))
                {
                    numberTo = int.Parse(model.numberSoldTo);
                }
                if (!string.IsNullOrEmpty(model.priceFrom))
                {
                    priceFrom = decimal.Parse(model.priceFrom);
                }
                if (!string.IsNullOrEmpty(model.priceTo))
                {
                    priceTo = decimal.Parse(model.priceTo);
                }
                if (!string.IsNullOrEmpty(model.doanhThuFrom))
                {
                    doanhThuFrom = decimal.Parse(model.doanhThuFrom);
                }
                if (!string.IsNullOrEmpty(model.doanhThuTo))
                {
                    doanhThuTo = decimal.Parse(model.doanhThuTo);
                }
                if (numberFrom > numberTo && numberFrom != null && numberTo != null)
                {
                    throw new Exception("Số lượng từ nhỏ hơn giá đến");
                }
                if (priceFrom > priceTo && priceFrom!=null && priceTo!=null)
                {
                    throw new Exception("Giá từ nhỏ hơn giá đến");
                }
                if (doanhThuFrom > doanhThuTo && doanhThuFrom != null && doanhThuTo != null)
                {
                    throw new Exception("Già từ nhỏ hơn giá đến");
                }

            }
            catch(Exception e)
            {
                return RedirectToAction("Index");
            }

            int selectYear = 0;
            int selectMonth = 0;
            DateTime selectedDate = new DateTime();
            DateTime firstDate = new DateTime();
            DateTime lastDate = new DateTime();


            if (model.selectedDay == "-1" && model.selectedMonth != "-1")
            {
                try
                {
                    selectYear = int.Parse(model.selectedYear);
                    selectMonth = int.Parse(model.selectedMonth);
                    firstDate = new DateTime(selectYear, selectMonth, 1);
                    lastDate = new DateTime(selectYear, selectMonth, DateTime.DaysInMonth(selectYear, selectMonth));
                }
                catch (Exception e)
                {
                    RedirectToAction("Index");
                }
                model.data = new OrderItemDAO().getDataDoanhThu(firstDate, lastDate, model.categoryName, 
                    productID, numberFrom, numberTo, priceFrom, priceTo, doanhThuFrom, doanhThuTo);
                
            }
            else if (model.selectedMonth == "-1")
            {
                try
                {
                    selectYear = int.Parse(model.selectedYear);
                }
                catch (Exception e)
                {
                    return RedirectToAction("Index");
                }
                firstDate = new DateTime(selectYear, 1, 1);
                lastDate = new DateTime(selectYear, 12, 31);
                model.data = new OrderItemDAO().getDataDoanhThu(firstDate, lastDate, model.categoryName,
                    productID, numberFrom, numberTo, priceFrom, priceTo, doanhThuFrom, doanhThuTo);
            }
            else
            {
                try
                {
                    selectedDate = DateTime.Parse(model.selectedDay);
                }
                catch (Exception e)
                {
                    return RedirectToAction("Index");
                }
                firstDate = selectedDate;
                lastDate = firstDate.AddDays(6);
                model.data = new OrderItemDAO().getDataDoanhThu(firstDate, lastDate, model.categoryName,
                    productID, numberFrom, numberTo, priceFrom, priceTo, doanhThuFrom, doanhThuTo);
            }
            return View(model);
        }
    }
}