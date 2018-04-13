using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.QuanLy.Models;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers
{
    public class OrderListController : Controller
    {
        // GET: QuanLy/OrderList
        public ActionResult Index()
        {
            var orderDAO = new OrderTotalDAO();
            var customerDao = new CustomerDAO();
            var statusDAO = new StatusDAO();
            var model = new ListModel();
            var lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID == 1).ToList();
            var items = new List<ListItemModel>();
            foreach (Order_total o in lstOrder)
            {
                var item = new ListItemModel
                {
                    orderId = o.Order_ID,
                    customer = customerDao.getCustomerById(o.Customer_ID).Customer_name,
                    date = o.Date_created,
                    delivery = o.Order_part.Count,
                    note = o.Note,
                    total = o.Total_price,
                    status = statusDAO.getStatus(o.Status_ID),
                    spanClass = "label-info"
                };
                items.Add(item);
            }
            model.statusId = 1;
            model.items = items;
            return View(model);
        }

        public ActionResult Complete()
        {
            var orderDAO = new OrderTotalDAO();
            var customerDao = new CustomerDAO();
            var statusDAO = new StatusDAO();
            var model = new ListModel();
            var lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID == 7).ToList();
            var items = new List<ListItemModel>();
            foreach (Order_total o in lstOrder)
            {
                var item = new ListItemModel
                {
                    orderId = o.Order_ID,
                    customer = customerDao.getCustomerById(o.Customer_ID).Customer_name,
                    date = o.Date_created,
                    delivery = o.Order_part.Count,
                    note = o.Note,
                    total = o.Total_price,
                    status = statusDAO.getStatus(o.Status_ID),
                    spanClass = "label-success"
                };
                items.Add(item);
            }
            model.statusId = 7;
            model.items = items;
            return View(model);
        }

        public ActionResult Cancel()
        {
            var orderDAO = new OrderTotalDAO();
            var customerDao = new CustomerDAO();
            var statusDAO = new StatusDAO();
            var model = new ListModel();
            var lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID == 8).ToList();
            var items = new List<ListItemModel>();
            foreach (Order_total o in lstOrder)
            {
                var item = new ListItemModel
                {
                    orderId = o.Order_ID,
                    customer = customerDao.getCustomerById(o.Customer_ID).Customer_name,
                    date = o.Date_created,
                    delivery = o.Order_part.Count,
                    note = o.Note,
                    total = o.Total_price,
                    status = statusDAO.getStatus(o.Status_ID),
                    spanClass = "label-danger"
                };
                items.Add(item);
            }
            model.statusId = 8;
            model.items = items;
            return View(model);
        }

        public ActionResult Store()
        {
            var orderDAO = new OrderTotalDAO();
            var customerDao = new CustomerDAO();
            var statusDAO = new StatusDAO();
            var model = new ListModel();
            var lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID == 2).ToList();
            var items = new List<ListItemModel>();
            foreach (Order_total o in lstOrder)
            {
                var item = new ListItemModel
                {
                    orderId = o.Order_ID,
                    customer = customerDao.getCustomerById(o.Customer_ID).Customer_name,
                    date = o.Date_created,
                    delivery = o.Order_part.Count,
                    note = o.Note,
                    total = o.Total_price,
                    status = statusDAO.getStatus(o.Status_ID),
                    spanClass = "label-warning"
                };
                items.Add(item);
            }
            model.statusId = 2;
            model.items = items;
            return View(model);
        }

        public ActionResult Processing()
        {
            var orderDAO = new OrderTotalDAO();
            var customerDao = new CustomerDAO();
            var statusDAO = new StatusDAO();
            var model = new ListModel();
            var lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID != 1 && x.Status_ID != 2 && x.Status_ID != 7 && x.Status_ID != 8).ToList();
            var items = new List<ListItemModel>();
            foreach (Order_total o in lstOrder)
            {
                var spanClass = "";
                var status = statusDAO.getStatus(o.Status_ID);
                if (o.Status_ID == 10 || o.Status_ID == 11)
                {
                    status = status.Substring(0, status.IndexOf("warning") - 1);
                    spanClass = "label-warning";
                }
                if (o.Status_ID == 3 || o.Status_ID == 4)
                {
                    spanClass = "label-primary";
                }
                if (o.Status_ID == 5 || o.Status_ID == 6)
                {
                    spanClass = "label-success";
                }
                if (o.Status_ID == 9)
                {
                    spanClass = "label-danger";
                }
                var item = new ListItemModel
                {
                    orderId = o.Order_ID,
                    customer = customerDao.getCustomerById(o.Customer_ID).Customer_name,
                    date = o.Date_created,
                    delivery = o.Order_part.Count,
                    note = o.Note,
                    total = o.Total_price,
                    status = status,
                    spanClass = spanClass
                };
                items.Add(item);
            }
            model.items = items;
            return View(model);
        }

        public ActionResult Search(ListModel model)
        {
            try
            {
                var orderDAO = new OrderTotalDAO();
                var lstOrder = new List<Order_total>();
                if(model.statusId == 12)
                {
                    lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID != 1 && x.Status_ID != 2 && x.Status_ID != 7 && x.Status_ID != 8).ToList();
                } else
                {
                    lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID == model.statusId).ToList();
                }                                
                if (!String.IsNullOrEmpty(model.orderId))
                {
                    lstOrder = lstOrder.Where(x => x.Order_ID.ToLower().Contains(model.orderId.ToLower())).ToList();
                }
                if (!String.IsNullOrEmpty(model.fromDate.ToString()))
                {
                    lstOrder = lstOrder.Where(x => x.Date_created > model.fromDate).ToList();
                }
                if (!String.IsNullOrEmpty(model.toDate.ToString()))
                {
                    lstOrder = lstOrder.Where(x => x.Date_created < model.toDate).ToList();
                }
                if (model.fromTotal > 0)
                {
                    lstOrder = lstOrder.Where(x => x.Total_price > model.fromTotal).ToList();
                }
                if (model.toTotal > 0)
                {
                    lstOrder = lstOrder.Where(x => x.Total_price < model.toTotal).ToList();
                }
                var items = new List<ListItemModel>();
                var customerDao = new CustomerDAO();
                var statusDAO = new StatusDAO();
                foreach (Order_total o in lstOrder)
                {
                    var spanClass = "";
                    var status = statusDAO.getStatus(o.Status_ID);
                    if (o.Status_ID == 1)
                    {
                        spanClass = "label-info";
                    }
                    else if (o.Status_ID == 3 || o.Status_ID == 4)
                    {
                        spanClass = "label-primary";
                    }
                    else if (o.Status_ID == 5 || o.Status_ID == 6 || o.Status_ID == 7)
                    {
                        spanClass = "label-success";
                    }
                    else if (o.Status_ID == 8 || o.Status_ID == 9)
                    {
                        spanClass = "label-danger";
                    }
                    else
                    {
                        if (o.Status_ID == 10 || o.Status_ID == 11)
                        {
                            status = status.Substring(0, status.IndexOf("warning") - 1);
                        }
                        spanClass = "label-warning";
                    }
                    var item = new ListItemModel
                    {
                        orderId = o.Order_ID,
                        customer = customerDao.getCustomerById(o.Customer_ID).Customer_name,
                        date = o.Date_created,
                        delivery = o.Order_part.Count,
                        note = o.Note,
                        total = o.Total_price,
                        status = status,
                        spanClass = spanClass
                    };
                    items.Add(item);
                }
                if (!String.IsNullOrEmpty(model.customer))
                {
                    items = items.Where(x => x.customer.ToLower().Contains(model.customer.ToLower())).ToList();
                }
                if (model.orderType > 0)
                {
                    if (model.orderType == 1)
                    {
                        items = items.Where(x => x.delivery == 1).ToList();
                    }
                    else
                    {
                        items = items.Where(x => x.delivery > 1).ToList();
                    }
                }
                model.items = items;
                if (model.statusId == 1 || model.statusId == 8)
                {
                    return PartialView("_searchPartialNormal", model);
                }
                else
                {
                    return PartialView("_searchPartialSpecial", model);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }

    }

}