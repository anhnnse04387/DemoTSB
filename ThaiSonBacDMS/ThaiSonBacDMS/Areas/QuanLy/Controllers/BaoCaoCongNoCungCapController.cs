using Models.DAO;
using Models.DAO_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.QuanLy.Models;
using ThaiSonBacDMS.Controllers;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers
{
    public class BaoCaoCongNoCungCapController : QuanLyBaseController
    {
        // GET: Quanly/BaoCaoCongNoCungCap
        public ActionResult Index()
        {
            BaoCaoCongNoCungCapModel model = new BaoCaoCongNoCungCapModel();
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
            return View(model);
        }

        public ActionResult ChangeData(BaoCaoCongNoCungCapModel model)
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
            model.supp_HanQuoc = new Dictionary<string, List<DataCongNoCungCap>>();
            model.supp_HanQuoc = new PIDAO().getDataLineChart(firstDate, lastDate, 3, model.selectedCategory);
            model.sumHanQuoc = model.supp_HanQuoc.Sum(x => x.Value.Sum(s => s.totalPrice));
            model.supp_LS = new Dictionary<string, List<DataCongNoCungCap>>();
            model.supp_LS = new PIDAO().getDataLineChart(firstDate, lastDate, 2, model.selectedCategory);
            model.sumLS = model.supp_LS.Sum(x => x.Value.Sum(s => s.totalPrice));
            model.supp_TSN = new Dictionary<string, List<DataCongNoCungCap>>();
            model.supp_TSN = new PIDAO().getDataLineChart(firstDate, lastDate, 1, model.selectedCategory);
            model.sumTNS = model.supp_TSN.Sum(x => x.Value.Sum(s => s.totalPrice));

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}