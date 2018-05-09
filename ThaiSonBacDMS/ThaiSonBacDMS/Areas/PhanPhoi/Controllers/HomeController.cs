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

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class HomeController : PhanPhoiBaseController
    {
        // GET: PhanPhoi/Home
        [HttpGet]
        public ActionResult Index()
        {
            OrderTotalDAO totalDAO = new OrderTotalDAO();
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
            decimal valueInMonth = (decimal) listTotal.Sum(x=>x.Total_price);
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
            var n = totalDAO.showNewestOrder(session.user_id);
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

            dataLineChartCurrentMonth = listTotal.GroupBy(x => x.Date_created).ToDictionary(x => x.Key.Day, x => Math.Round( (decimal)x.Sum(s => s.Total_price)/ 10000000,2));
            dataLineChartOrderCurrentMonth = listTotal.GroupBy(x => x.Date_created).ToDictionary(x => x.Key.Day, x => (int)x.Count());
            //
            if(listTotal.Count ==0)
            {
                dataLineChartCurrentMonth.Add(1, 0);
                dataLineChartOrderCurrentMonth.Add(1, 0);
                dataLineChartCurrentMonth.Add(lastDayOfMonth.Day, 0);
                dataLineChartOrderCurrentMonth.Add(lastDayOfMonth.Day, 0);
            }
            else if(listTotal.First().Date_created.Day != 1)
            {
                dataLineChartCurrentMonth.Add(1, 0);
                dataLineChartOrderCurrentMonth.Add(1, 0);
            }
            model.dataLineChartCurrentMonth = dataLineChartCurrentMonth.OrderBy(x=>x.Key).ToDictionary(x=>x.Key, x=>x.Value);
            model.dataLineChartOrderCurrentMonth = dataLineChartOrderCurrentMonth.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value); ;
            //

            Dictionary<int, decimal> dataLineChartPreviousMonth = new Dictionary<int, decimal>();
            Dictionary<int, int> dataLineChartPreviousOrderMonth = new Dictionary<int, int>();
            var firstMonthPrevious = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
            var lastMonthPrevious = firstMonthPrevious.AddMonths(1).AddDays(-1);
            var listTotalPrevious = totalDAO.getOrderByDateCreated(firstMonthPrevious, lastMonthPrevious);

            dataLineChartPreviousMonth = listTotalPrevious.GroupBy(x => x.Date_created).ToDictionary(x => x.Key.Day, x => Math.Round((decimal)x.Sum(s => s.Total_price) / 10000000, 2));
            dataLineChartPreviousOrderMonth = listTotalPrevious.GroupBy(x => x.Date_created).ToDictionary(x => x.Key.Day, x => (int)x.Count());
            if(listTotalPrevious.Count == 0)
            {
                dataLineChartPreviousOrderMonth.Add(1, 0);
                dataLineChartPreviousMonth.Add(1, 0);
                dataLineChartPreviousOrderMonth.Add(lastMonthPrevious.Day, 0);
                dataLineChartPreviousMonth.Add(lastMonthPrevious.Day, 0);
            }
            else if (listTotalPrevious.First().Date_created.Day != 1)
            {
                dataLineChartPreviousOrderMonth.Add(1, 0);
                dataLineChartPreviousMonth.Add(1, 0);
            }
            
            model.dataLineChartPreviousMonth = dataLineChartPreviousMonth.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value); ;
            model.dataLineChartOrderPreviousMonth = dataLineChartPreviousOrderMonth.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value); ;

            //count value in previous month
            decimal valueInPreviousMonth = 0;

            valueInPreviousMonth = (decimal) listTotalPrevious.Sum(x=>x.Total_price);
            model.diffrentValueMonth = 0;
            model.valueFlag = false;
            if (valueInMonth >= valueInPreviousMonth)
            {
                model.valueFlag = true;
                model.diffrentValueMonth = valueInPreviousMonth > 0 ? ((valueInMonth - valueInPreviousMonth) / valueInPreviousMonth * 100) : 100;
            }else
            {
                model.diffrentValueMonth = valueInMonth > 0 ? (valueInPreviousMonth - valueInMonth) / valueInMonth * 100 : 100;
            }
            //different order of month
            model.diffrentOrderMonth = 0;
            model.orderFlag = false;
            var orderInPreviousMonth = dataLineChartPreviousOrderMonth.Values.Sum();
            var totalInMonth = dataLineChartOrderCurrentMonth.Values.Sum();
            if (totalInMonth >= orderInPreviousMonth)
            {
                model.orderFlag = true;
                model.diffrentOrderMonth = valueInPreviousMonth > 0 ? ((valueInMonth - valueInPreviousMonth) / valueInPreviousMonth * 100) : 100;
            }
            else
            {
                model.diffrentOrderMonth = valueInMonth > 0 ? ((valueInPreviousMonth - valueInMonth) / valueInPreviousMonth * 100) : 100;
            }

            //Top Selling Product
            model.topSellingCurrentMonth = orderItemDAO.
                getTopSellingProductInMonth(firstDayOfMonth, lastDayOfMonth);
            model.topSellingPreviousMonth = orderItemDAO.
                getTopSellingProductInMonth(firstMonthPrevious, lastMonthPrevious);


            //Top Selling Category
            Dictionary<string, int> topSellingCategoryCurrent = new Dictionary<string, int>();
            topSellingCategoryCurrent = orderItemDAO.getTopSellingCategory(firstDayOfMonth, DateTime.Now);
            List<TopSellingCategory> listTSC = new List<TopSellingCategory>();

            foreach(var item in topSellingCategoryCurrent) {
                TopSellingCategory tsc = new TopSellingCategory();
                tsc.categoryName = new CategoryDAO().getCategoryById(item.Key).Category_name;
                tsc.numberCategory = item.Value;
                tsc.diffrentPercent = 0;
                int valuePrevious = orderItemDAO.
                    getCategoryQuantityByDate(item.Key, firstMonthPrevious, DateTime.Now.AddMonths(-1));
                if (item.Value >= valuePrevious)
                {
                    tsc.categoryFlag = true;
                    tsc.diffrentPercent = valuePrevious == 0 ? 100 : (decimal)(item.Value - valuePrevious) / valuePrevious * 100;
                }
                else
                {
                    tsc.categoryFlag = false;
                    tsc.diffrentPercent = item.Value == 0 ? 100 : (valuePrevious - item.Value) / item.Value * 100;
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
                listNoti.AddRange(notiDAO.getByUserID(session.user_id));
                listNoti.AddRange(notiDAO.getByRoleID((int) session.roleSelectedID));
                return PartialView(listNoti);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                RedirectToAction("Index");
            }
            return PartialView();
        }

        public ActionResult ChangeStatusNote(string link, int notiID)
        {
            link = "/ChiTietPhieu/Index?orderId=O1";
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            new NotificationDAO().changeStatus(notiID);
            switch(session.roleSelectedID)
            {
                case 1:
                    link = "/QuanTri" + link;
                    break;
                case 2:
                    link = "/QuanLy" + link;
                    break;
                case 3:
                    link = "/PhanPhoi" + link;
                    break;
                case 4:
                    link = "/HangHoa" + link;
                    break;
                case 5:
                    link = "/KeToan" + link;
                    break;
                default:
                    break;
            }
            return Redirect(link);
        }

        [ChildActionOnly]
        public PartialViewResult NoteHeader()
        {
            
            List<string> lstOrderPartName = new OrderPartDAO()
                .getByCreatedDate(new DateTime(2018,4,1), new DateTime(2018,4,10)).Select(x=>"Đơn " + x.Order_part_ID + " đang chờ được xử lí").ToList();
            if(lstOrderPartName.Count == 0)
            {
                lstOrderPartName.Add("Không có đơn nào phải xử lí trong hôm nay");
            }
            return PartialView(lstOrderPartName);
        }

        public ActionResult outConfirmRole()
        {
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            session.roleSelectedID = 0;
            return RedirectToAction("ConfirmRole", "ConfirmRole", new { area = "" });
        }
    }
}