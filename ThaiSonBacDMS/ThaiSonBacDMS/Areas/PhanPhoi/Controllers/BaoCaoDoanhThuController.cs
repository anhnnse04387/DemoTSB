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
    public class BaoCaoDoanhThuController : BaseController
    {
        // GET: PhanPhoi/BaoCaoDoanhThu
        [HttpGet]
        public ActionResult Index()
        {
            BaoCaoDoanhThuModel model = new BaoCaoDoanhThuModel();
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

        [HttpPost]
        public ActionResult Index(BaoCaoDoanhThuModel model)
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
                model.dataLineChart = returnData(firstDate, lastDate, model.selectedCategory);
                model.dataPieChart = returnPieChartData(firstDate, lastDate);


            }
            else if(model.selectedMonth == "-1")
            {
                try
                {
                    selectYear = int.Parse(model.selectedYear);
                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                    return RedirectToAction("Index");
                }
                firstDate = new DateTime(selectYear, 1, 1);
                lastDate = new DateTime(selectYear, 12, 31);
                model.dataLineChart = returnData(firstDate, lastDate, model.selectedCategory);
                model.dataPieChart = returnPieChartData(firstDate, lastDate);
            }
            else
            {
                try
                {
                    selectedDate = DateTime.Parse(model.selectedDay);
                }
                catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                    return RedirectToAction("Index");
                }
                firstDate = selectedDate;
                lastDate = firstDate.AddDays(6);
                model.dataLineChart = returnData(firstDate, lastDate, model.selectedCategory);
                model.dataPieChart = returnPieChartData(firstDate, lastDate);
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private List<DataLineChart> returnData(DateTime firstDate, DateTime lastDate, string categoryID)
        {
            Dictionary<DateTime, List<decimal>> listData = new OrderItemDAO().getChartData(firstDate, lastDate, categoryID);
            List<DataLineChart> listDataLineChart = new List<DataLineChart>();
            foreach (var item in listData)
            {
                DataLineChart data = new DataLineChart();
                data.displayTime = item.Key.ToString("dd/MM");
                data.nhapVon = item.Value.ElementAt(0);
                data.xuatVon = item.Value.ElementAt(1);
                data.banChoKhach = item.Value.ElementAt(2);
                listDataLineChart.Add(data);
            }
            return listDataLineChart;
        }

        private List<DataPieChart> returnPieChartData(DateTime firstDate, DateTime lastDate) 
        {
            List<DataPieChart> dataPieChart = new List<DataPieChart>();
            foreach (var item in new OrderItemDAO().getPieChartData(firstDate, lastDate))
            {
                var pieChartData = new DataPieChart();
                pieChartData.categoryName = item.Key;
                pieChartData.numberSold = (int)item.Value.ElementAt(0);
                pieChartData.nhapVon = item.Value.ElementAt(1);
                pieChartData.xuatVon = item.Value.ElementAt(2);
                pieChartData.banChoKhach = item.Value.ElementAt(3);
                pieChartData.loiNhuan = item.Value.ElementAt(3) - item.Value.ElementAt(2);
                dataPieChart.Add(pieChartData);
            }
            return dataPieChart;
        }


    }
}