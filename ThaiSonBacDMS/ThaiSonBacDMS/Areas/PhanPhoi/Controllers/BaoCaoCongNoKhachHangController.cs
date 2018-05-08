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
    public class BaoCaoCongNoKhachHangController : PhanPhoiBaseController
    {
        public ActionResult Index()
        {
            BaoCaoCongNoKhachHangModel model = new BaoCaoCongNoKhachHangModel();
            //set year in db
            model.listShowYear = new List<SelectListItem>();
            foreach (var i in new OrderTotalDAO().getListYear())
            {
                var item = new SelectListItem { Text = i.ToString(), Value = i.ToString() };
                model.listShowYear.Add(item);
            }
            //set category
            model.listCategory = new List<SelectListItem>();
            model.listCategory.Add(new SelectListItem { Text = "Tất cả", Value = "-1" });
            foreach (var i in new CategoryDAO().getLstCate())
            {
                var item = new SelectListItem { Text = i.Category_name.ToString(), Value = i.Category_ID.ToString() };
                model.listCategory.Add(item);
            }
            //set table
            model.listCongNo = new List<CongNoKhachHang>();
            foreach (var x in new CustomerDAO().getTop10CustomerDebt())
            {
                model.listCongNo.Add(new CongNoKhachHang(x.Customer_name, x.Delivery_address, (decimal)x.Current_debt, x.Customer_ID));
            }
            return View(model);
        }

        public ActionResult ChangeData(BaoCaoCongNoKhachHangModel model)
        {
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
            model.dataCongNo = new OrderItemDAO().getDataCongNoKhachHang(firstDate, lastDate, model.selectedCategory);
            model.totalPrice = model.dataCongNo.Sum(x=>x.Value.Sum(s=>s.totalPrice));
            
            
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}