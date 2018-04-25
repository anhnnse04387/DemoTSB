﻿using Models.DAO;
using Models.DAO_Model;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;
using ThaiSonBacDMS.Controllers;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class BaoCaoChiTietCongNoController : PhanPhoiBaseController
    {
        // GET: PhanPhoi/BaoCaoChiTietDoanhThu
        [HttpGet]
        public ActionResult Index(int supplierID)
        {
            ChiTietNoCungCapModel model = new ChiTietNoCungCapModel();
            CategoryDAO daoCate = new CategoryDAO();
            //set year in db
            model.listShowYear = new List<SelectListItem>();
            foreach (var i in new OrderTotalDAO().getListYear())
            {
                var item = new SelectListItem { Text = i.ToString(), Value = i.ToString() };
                model.listShowYear.Add(item);
            }
            //set category in db
            List<Category> lstTemp = new List<Category>();
            model.lstCategorySearch = new List<SelectListItem>();
            lstTemp = daoCate.getLstCate();
            if (lstTemp.Count != 0)
            {
                foreach (Category itemCate in lstTemp)
                {
                    model.lstCategorySearch.Add(new SelectListItem { Text = itemCate.Category_name, Value = itemCate.Category_ID });
                }
            }
            //get supplier
            Supplier supp = new Supplier();
            try
            {
                supp = new SupplierDAO().getSupplierById(supplierID);
            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index", "BaoCaoCongNoCungCap", new { area = "PhanPhoi" });
            }
            model.supp = supp;
            //display data
            DateTime firstDayOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            var dataLst = new PIDAO().getDataNoCungCap(firstDayOfWeek, firstDayOfWeek.AddDays(6), supplierID, 
                null, null, null, null, null, null);

            var returnValue = from d in dataLst
                              group d by d.categoryName into g
                              select new
                              {
                                  categoryName = g.Key,
                                  data = g.ToList()
                              };
            Dictionary<string, List<DataCongNoCungCap>> dataDic = new Dictionary<string, List<DataCongNoCungCap>>();
            foreach (var item in returnValue)
            {
                dataDic.Add(item.categoryName, item.data);
            }
            model.data = dataDic;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ChiTietNoCungCapModel model, int supplierID)
        {
            CategoryDAO daoCate = new CategoryDAO();

            int? numberFrom = null;
            int? numberTo = null;
            decimal? priceFrom = null;
            decimal? priceTo = null;
            model.errorString = "";
            model.data = new Dictionary<string, List<DataCongNoCungCap>>();
            List<DataCongNoCungCap> data = new List<DataCongNoCungCap>();
            //set year in db
            model.listShowYear = new List<SelectListItem>();
            foreach (var i in new OrderTotalDAO().getListYear())
            {
                var item = new SelectListItem { Text = i.ToString(), Value = i.ToString() };
                model.listShowYear.Add(item);
            }
            //set category in db
            List<Category> lstTemp = new List<Category>();
            model.lstCategorySearch = new List<SelectListItem>();
            lstTemp = daoCate.getLstCate();
            if (lstTemp.Count != 0)
            {
                foreach (Category itemCate in lstTemp)
                {
                    model.lstCategorySearch.Add(new SelectListItem { Text = itemCate.Category_name, Value = itemCate.Category_ID });
                }
            }
            //get supplier
            Supplier supp = new Supplier();
            try
            {
                supp = new SupplierDAO().getSupplierById(supplierID);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index", "BaoCaoCongNoCungCap", new { area = "PhanPhoi" });
            }
            model.supp = supp;
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
                if (numberFrom > numberTo && numberFrom != null && numberTo != null)
                {
                    throw new Exception("Số lượng từ nhỏ hơn giá đến");
                }
                if (priceFrom > priceTo && priceFrom != null && priceTo != null)
                {
                    throw new Exception("Giá từ nhỏ hơn giá đến");
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                model.errorString = e.Message;
                return View(model);
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
            var dataLst = new PIDAO().getDataNoCungCap(firstDate, lastDate,supplierID, model.categoryName,
                    model.productCode, numberFrom, numberTo, priceFrom, priceTo);
            var returnValue = from d in dataLst
                              group d by d.categoryName into g
                              select new
                              {
                                  categoryName = g.Key,
                                  data = g.ToList()
                              };
            Dictionary<string, List<DataCongNoCungCap>> dataDic = new Dictionary<string, List<DataCongNoCungCap>>();
            foreach (var item in returnValue)
            {
                dataDic.Add(item.categoryName, item.data);
            }
            model.data = dataDic;
            return View(model);
        }
    }
}