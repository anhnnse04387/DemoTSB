using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.KeToan.Models;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.KeToan.Controllers
{
    public class HomeController : KeToanBaseController
    {
        // GET: PhanPhoi/OrderList
        public ActionResult Index()
        {
            var orderDAO = new OrderTotalDAO();
            var customerDao = new CustomerDAO();
            var statusDAO = new StatusDAO();
            var model = new ListModel();
            var lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID == 3 || x.Status_ID == 10 || x.Status_ID == 11).ToList();
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
                else
                {
                    spanClass = "label-primary";
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

        public ActionResult Release()
        {
            var orderDAO = new OrderTotalDAO();
            var customerDao = new CustomerDAO();
            var statusDAO = new StatusDAO();
            var model = new ListModel();
            var lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID == 4).ToList();
            var items = new List<ListItemModel>();
            foreach (Order_total o in lstOrder)
            {
                var status = statusDAO.getStatus(o.Status_ID);
                var item = new ListItemModel
                {
                    orderId = o.Order_ID,
                    customer = customerDao.getCustomerById(o.Customer_ID).Customer_name,
                    date = o.Date_created,
                    delivery = o.Order_part.Count,
                    note = o.Note,
                    total = o.Total_price,
                    status = status,
                    spanClass = "label-success"
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
                if (model.statusId == 12)
                {
                    lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID > 3 && x.Status_ID < 9).ToList();
                }
                else if (model.statusId == 13)
                {
                    lstOrder = orderDAO.getLstOrder().Where(x => x.Status_ID == 3 || x.Status_ID == 10 || x.Status_ID == 11).ToList();
                }
                else
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
                return PartialView("_searchPartial", model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }

        public ActionResult ChangeStatusNote(string link, int notiID)
        {
            link = "/ChiTietPhieu/Index?orderId=O1";
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            new NotificationDAO().changeStatus(notiID);
            switch (session.roleSelectedID)
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
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                RedirectToAction("Index");
            }
            return PartialView();
        }
    }

}