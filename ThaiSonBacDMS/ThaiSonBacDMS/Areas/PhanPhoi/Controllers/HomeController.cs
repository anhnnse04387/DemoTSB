using Models.DAO;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;
using ThaiSonBacDMS.Common;
using ThaiSonBacDMS.Controllers;
using Models.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class HomeController : BaseController
    {
        // GET: PhanPhoi/Home
        [HttpGet]
        public ActionResult Index()
        {
            OrderTotalDAO totalDAO = new OrderTotalDAO();
            ProductDAO productDAO = new ProductDAO();
            CustomerDAO customerDAO = new CustomerDAO();
            OrderItemDAO orderItemDAO = new OrderItemDAO();
            HomeModel model = new HomeModel();
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var listTotal = totalDAO.getOrderByDateCreated(firstDayOfMonth, lastDayOfMonth); 
            //return order in month
            var orderInMonth = listTotal.Count;
            model.orderInMonth = orderInMonth;
            //return value in month
            int valueInMonth = 0;
            foreach(var item in listTotal)
            {
                valueInMonth += (int) item.Total_price;
            }
            model.valueInMonth = valueInMonth;
            //return total product in month
            var prodInMonth = orderItemDAO.countNumberProductSoldMonth(firstDayOfMonth, lastDayOfMonth);
            model.prodInMonth = prodInMonth;
            //return number of new customer
            var numberCustomer = customerDAO.getCustomerByDateCreated(firstDayOfMonth, lastDayOfMonth).Count;
            model.numberCustomer = numberCustomer;

            //table for newest order
            model.newestOrderList = new List<NewsetOrder>();
            var session = (UserSession)Session[CommonConstants.USER_SESSION]; 
            var n = totalDAO.showNewestOrder(int.Parse(session.user_id));
            foreach(var item in n)
            {
                NewsetOrder no = new NewsetOrder();
                no.orderID = item.Order_ID;
                no.numberBox = orderItemDAO.countNumberProduct(item.Order_ID);
                no.numberProduct = orderItemDAO.countNumberBox(item.Order_ID);
                no.totalPrice = (decimal) item.Total_price;
                no.customerName = customerDAO.getCustomerById(item.Customer_ID).Customer_name;
                no.status = new StatusDAO().getStatus(item.Status_ID);
                model.newestOrderList.Add(no);
            }
            //line chart
            Dictionary<int, decimal> dataLineChartCurrentMonth = new Dictionary<int, decimal>();
            Dictionary<int, int> dataLineChartOrderCurrentMonth = new Dictionary<int, int>();
            if(listTotal.Count != 0 && listTotal.First().Date_created.Day != 1)
            {
                dataLineChartCurrentMonth.Add(1, 0);
                dataLineChartOrderCurrentMonth.Add(1, 0);
            }
            foreach(var item in listTotal)
            {
                if(dataLineChartCurrentMonth.ContainsKey(item.Date_created.Day))
                {
                    dataLineChartCurrentMonth[item.Date_created.Day] += (decimal)item.Total_price;
                }else
                {
                    dataLineChartCurrentMonth.Add(item.Date_created.Day, (decimal)item.Total_price);
                }

                if(dataLineChartOrderCurrentMonth.ContainsKey(item.Date_created.Day))
                {
                    dataLineChartOrderCurrentMonth[item.Date_created.Day] += 1;
                }else
                {
                    dataLineChartOrderCurrentMonth.Add(item.Date_created.Day, 1);
                }
            }
            model.dataLineChartCurrentMonth = dataLineChartCurrentMonth;
            model.dataLineChartOrderCurrentMonth = dataLineChartOrderCurrentMonth;

            Dictionary<int, decimal> dataLineChartPreviousMonth = new Dictionary<int, decimal>();
            Dictionary<int, int> dataLineChartPreviousOrderMonth = new Dictionary<int, int>();
            var firstMonthPrevious = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
            var lastMonthPrevious = firstMonthPrevious.AddMonths(1).AddDays(-1);
            var listTotalPrevious = totalDAO.getOrderByDateCreated(firstMonthPrevious, lastMonthPrevious);
            if (listTotal.Count != 0 && listTotalPrevious.First().Date_created.Day != 1)
            {
                dataLineChartPreviousMonth.Add(1, 0);
                dataLineChartPreviousOrderMonth.Add(1, 0);
            }
            
            foreach (var item in listTotalPrevious)
            {
                if (dataLineChartPreviousMonth.ContainsKey(item.Date_created.Day))
                {
                    dataLineChartPreviousMonth[item.Date_created.Day] += (decimal)item.Total_price;
                }
                else
                {
                    dataLineChartPreviousMonth.Add(item.Date_created.Day, (decimal)item.Total_price);
                }

                if (dataLineChartPreviousOrderMonth.ContainsKey(item.Date_created.Day))
                {
                    dataLineChartPreviousOrderMonth[item.Date_created.Day] += 1;
                }
                else
                {
                    dataLineChartPreviousOrderMonth.Add(item.Date_created.Day, 1);
                }
            }
            if(!dataLineChartPreviousMonth.ContainsKey(lastMonthPrevious.Day))
            {
                dataLineChartPreviousMonth.Add(lastMonthPrevious.Day, 0);
            }
            if (!dataLineChartPreviousOrderMonth.ContainsKey(lastMonthPrevious.Day))
            {
                dataLineChartPreviousOrderMonth.Add(lastMonthPrevious.Day, 0);
            }
            model.dataLineChartPreviousMonth = dataLineChartPreviousMonth;
            model.dataLineChartOrderPreviousMonth = dataLineChartPreviousOrderMonth;

            //count value in previous month
            int valueInPreviousMonth = 0;
            foreach (var item in listTotalPrevious)
            {
                valueInPreviousMonth += (int)item.Total_price;
            }
            model.diffrentValueMonth = 0;
            model.valueFlag = false;
            if (valueInMonth >= valueInPreviousMonth)
            {
                model.valueFlag = true;
                model.diffrentValueMonth = valueInPreviousMonth > 0 ? ((valueInMonth - valueInPreviousMonth) / valueInPreviousMonth * 100) : valueInMonth;
            }else
            {
                model.diffrentValueMonth = valueInMonth > 0 ? (valueInPreviousMonth - valueInMonth) / valueInMonth * 100 : valueInPreviousMonth;
            }
            //different order of month
            model.diffrentOrderMonth = 0;
            model.orderFlag = false;
            var orderInPreviousMonth = dataLineChartPreviousOrderMonth.Values.Sum();
            var totalInMonth = dataLineChartOrderCurrentMonth.Values.Sum();
            if (totalInMonth >= orderInPreviousMonth)
            {
                model.orderFlag = true;
                model.diffrentValueMonth = valueInPreviousMonth > 0 ? ((valueInMonth - valueInPreviousMonth) / valueInPreviousMonth * 100) : 0;
            }
            else
            {
                model.diffrentValueMonth = valueInMonth > 0 ? ((valueInPreviousMonth - valueInMonth) / valueInPreviousMonth * 100) : 0;
            }

            //Top Selling Product
            model.topSellingCurrentMonth = orderItemDAO.
                getTopSellingProductInMonth(firstDayOfMonth, lastDayOfMonth);
            model.topSellingPreviousMonth = orderItemDAO.
                getTopSellingProductInMonth(firstMonthPrevious, lastMonthPrevious);


            //Top Selling Category
            Dictionary<string, int> topSellingCategoryCurrent = new Dictionary<string, int>();
            topSellingCategoryCurrent = orderItemDAO.getTopSellingCategory(firstDayOfMonth, lastDayOfMonth);
            Dictionary<string, int> topSellingCategoryPrevious = new Dictionary<string, int>();
            topSellingCategoryPrevious = orderItemDAO.getTopSellingCategory(firstMonthPrevious, lastMonthPrevious);
            List<TopSellingCategory> listTSC = new List<TopSellingCategory>();

            foreach(var item in topSellingCategoryCurrent) {
                TopSellingCategory tsc = new TopSellingCategory();
                tsc.categoryName = new CategoryDAO().getCategoryById(item.Key).Category_name;
                tsc.numberCategory = item.Value;
                tsc.diffrentPercent = 0;
                int valuePrevious = orderItemDAO.
                    getCategoryQuantityByDate(item.Key, firstMonthPrevious, lastMonthPrevious);
                if (item.Value >= valuePrevious && valuePrevious != 0)
                {
                    tsc.categoryFlag = true;
                    tsc.diffrentPercent = (decimal) (item.Value - valuePrevious) / valuePrevious * 100;
                }else if(item.Value != 0)
                {
                    tsc.categoryFlag = false;
                    tsc.diffrentPercent = (valuePrevious - item.Value) / item.Value * 100;
                }
                listTSC.Add(tsc);
            }
            model.listTopSellingCate = listTSC;

            return View(model);
        }

        [ChildActionOnly]
        public PartialViewResult NotificationHeader()
        {
            try
            {
                var notiDAO = new NotificationDAO();
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                List<Notification> listNoti = new List<Notification>();
                listNoti = notiDAO.getByUserID(session.user_id);
                return PartialView(listNoti);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                RedirectToAction("Index");
            }
            return PartialView();
        }
        [ChildActionOnly]
        public PartialViewResult NoteEdit()
        {
            try
            {
                var noteDAO = new NoteDAO();
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                var content = noteDAO.getNotebyAccount(session.accountID);

                return PartialView(content);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                RedirectToAction("Index");
            }
            return PartialView();
        }

        [HttpPost]
        public JsonResult NoteEdit(int accID, string content)
        {
            try
            {
                var noteDAO = new NoteDAO();
                noteDAO.editNotebyAccount(accID, content);
                return Json(content, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e) 
            {
                System.Diagnostics.Debug.WriteLine(e);
                RedirectToAction("Index");
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public PartialViewResult NoteHeader()
        {
            var noteDAO = new NoteDAO();
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            var content = String.Empty;
            string[] lines = new string[] { };
            try
            {
                content = noteDAO.getNotebyAccount(session.accountID).Contents;
                if (content != null && !string.IsNullOrEmpty(content))
                {

                    lines = content.Trim().Split(Environment.NewLine.ToCharArray());
                }
                else
                {
                    lines = new string[] { "Hãy điền ghi chú vào đây" };
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                RedirectToAction("Index");
            }
            return PartialView(lines);
        }
    }
}