using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class OrderTimeLineController : PhanPhoiBaseController
    {
        // GET: PhanPhoi/OrderTimeLine
        public ActionResult Index()
        {
            OrderTimelineModel model = new OrderTimelineModel();
            List<Order_part> listOrderPart = new List<Order_part>();
            listOrderPart = new OrderPartDAO().getByCreatedDate(new DateTime(2018, 4, 1), new DateTime(2018, 4, 10));
            List<OrderPartInfo> returnModel = listOrderPart.OrderBy(x => x.Date_created).Select((x, index) => new OrderPartInfo
            {
                indexOf = index,
                orderName = x.Order_part_ID,
                customerName = new CustomerDAO().getCustomerById(x.Customer_ID).Customer_name,
                dateCreated = x.Date_created,
                dateCompleted = (DateTime) x.Request_stockout_date,
                userName = new UserDAO().getByID(x.Sales_user_ID).User_name,
                status = x.Status_ID == 7 ? "completed" : "",
            }).ToList();

            model.ListOpInfo = returnModel;
            model.fromDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday).ToString("dd/MM/yyyy");
            model.toDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday).AddDays(6).ToString("dd/MM/yyyy");
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(OrderTimelineModel model)
        {
            try
            {
                DateTime fromDate = DateTime.ParseExact(model.fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime toDate = DateTime.ParseExact(model.toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                List<Order_part> listOrderPart = new List<Order_part>();
                listOrderPart = new OrderPartDAO().getByCreatedDate(fromDate, toDate);
                if(!string.IsNullOrEmpty(model.orderName))
                {
                    listOrderPart = listOrderPart.Where(x => x.Order_part_ID.Contains(model.orderName)).ToList();
                }
                List<OrderPartInfo> returnModel = listOrderPart.OrderBy(x => x.Date_created).Select((x, index) => new OrderPartInfo
                {
                    indexOf = index,
                    orderName = x.Order_part_ID,
                    customerName = new CustomerDAO().getCustomerById(x.Customer_ID).Customer_name,
                    dateCreated = x.Date_created,
                    dateCompleted = x.Date_completed,
                    userName = new UserDAO().getByID(x.Sales_user_ID).User_name,
                    status = x.Status_ID == 1 ? "completed" : "",
                }).ToList();

                model.ListOpInfo = returnModel;
                model.fromDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday).ToString("dd/MM/yyyy");
                model.toDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday).AddDays(6).ToString("dd/MM/yyyy");
                return View(model);
            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult DetailProduct(int productID)
        {
            OrderTimelineModel model = new OrderTimelineModel();
            
            List<Order_items> listOrderItem = new OrderItemDAO().getProductByExportDate(DateTime.Now, productID);
            model.ListOpInfo = listOrderItem.Select((x,index) => new OrderPartInfo
            {
                indexOf = index,
                orderName = x.Order_part_ID == null ? x.Order_ID : x.Order_part_ID,
                dateCompleted = new OrderPartDAO().getByName(x.Order_part_ID == null ? x.Order_ID : x.Order_part_ID).Request_stockout_date,
                quantity = x.Quantity == null ? 0: (int) x.Quantity,
            }).ToList();
            if(model.ListOpInfo.Count!=0)
            {
                model.ListOpInfo.First().status = "completed";
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult DetailProduct(OrderTimelineModel model, int productID)
        {
            DateTime fromDate = DateTime.ParseExact(model.fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<Order_items> listOrderItem = new OrderItemDAO().getProductByExportDate(fromDate, productID);
            model.ListOpInfo = listOrderItem.Select((x, index) => new OrderPartInfo
            {
                indexOf = index,
                orderName = x.Order_part_ID == null ? x.Order_ID : x.Order_part_ID,
                dateCompleted = new OrderPartDAO().getByName(x.Order_part_ID == null ? x.Order_ID : x.Order_part_ID).Request_stockout_date,
                quantity = x.Quantity == null ? 0 : (int)x.Quantity,
            }).ToList();
            if (model.ListOpInfo.Count != 0)
            {
                model.ListOpInfo.First().status = "completed";
            }
            if (!string.IsNullOrEmpty(model.orderName))
            {
                model.ListOpInfo = model.ListOpInfo.Where(x => x.orderName.Contains(model.orderName)).ToList();
            }
            return View(model);
        }
    }
}