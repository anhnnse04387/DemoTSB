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
            //display data
            var dataLst = new OrderItemDAO().getDataDoanhThu(DateTime.Now, DateTime.Now.AddDays(6), null,
                    null, null, null, null, null, null, null);
            var returnValue = from d in dataLst
                              group d by d.categoryName into g
                              select new
                              {
                                  categoryName = g.Key,
                                  data = g.ToList()
                              };
            Dictionary<string, List<DataChiTietDoanhThu>> dataDic = new Dictionary<string, List<DataChiTietDoanhThu>>();
            foreach (var item in returnValue)
            {
                dataDic.Add(item.categoryName, item.data);
            }
            model.data = dataDic;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ChiTietBaoCaoDoanhThu model)
        {
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
                System.Diagnostics.Debug.WriteLine(e);
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
                    System.Diagnostics.Debug.WriteLine(e);
                    RedirectToAction("Index");
                }
                
            }
            else if (model.selectedMonth == "-1")
            {
                try
                {
                    selectYear = int.Parse(model.selectedYear);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                    return RedirectToAction("Index");
                }
                firstDate = new DateTime(selectYear, 1, 1);
                lastDate = new DateTime(selectYear, 12, 31);
            }
            else
            {
                try
                {
                    selectedDate = DateTime.Parse(model.selectedDay);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                    return RedirectToAction("Index");
                }
                firstDate = selectedDate;
                lastDate = firstDate.AddDays(6);
                
            }
            var dataLst = new OrderItemDAO().getDataDoanhThu(firstDate, lastDate, model.categoryName,
                    model.productCode, numberFrom, numberTo, priceFrom, priceTo, doanhThuFrom, doanhThuTo);
            var returnValue = from d in dataLst
                              group d by d.categoryName into g
                              select new
                              {
                                  categoryName = g.Key,
                                  data = g.ToList()
                              };
            Dictionary<string, List<DataChiTietDoanhThu>> dataDic = new Dictionary<string, List<DataChiTietDoanhThu>>();
            foreach(var item in returnValue)
            {
                dataDic.Add(item.categoryName, item.data);
            }
            model.data = dataDic;
            return View(model);
        }
    }
}