using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.HangHoa.Models;

namespace ThaiSonBacDMS.Areas.HangHoa.Controllers
{
    public class OrderListController : Controller
    {
        // GET: HangHoa/OrderList
        public ActionResult Index(OrderListModel model)
        {
            try
            {
                List<Order_part> lstOrder = new OrderPartDAO().getAllOrderPartByStatus(3);
                lstOrder.AddRange(new OrderPartDAO().getAllOrderPartByStatus(4));
                model.listOrderPending = lstOrder.Select((x, index) => new OrderListPending
                {
                    indexOf = index + 1,
                    orderID = x.Order_part_ID,
                    customerName = new CustomerDAO().getCustomerById(x.Customer_ID).Customer_name,
                    invoiceNumber = x.Invoice_number,
                    statusName = new StatusDAO().getStatus((byte)x.Status_ID),
                    takeInvoice = x.Date_take_invoice == null ? false : true,
                    takeBallot = x.Date_take_ballot == null ? false : true,
                    dateExport = (DateTime)x.Date_created,
                    note = x.Note,
                }).ToList();
                if (!string.IsNullOrEmpty(model.orderID))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.orderID.Contains(model.orderID)).ToList();
                }
                if (!string.IsNullOrEmpty(model.customerName))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.customerName.Contains(model.customerName)).ToList();
                }
                if (!string.IsNullOrEmpty(model.invoice_number))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.invoiceNumber.Contains(model.invoice_number)).ToList();
                }
                if (!string.IsNullOrEmpty(model.statusName))
                {
                    model.listOrderPending = model.listOrderPending.Where(x => x.statusName == model.statusName).ToList();
                }
                if (!string.IsNullOrEmpty(model.fromDate))
                {
                    DateTime fromDate = DateTime.ParseExact(model.fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    model.listOrderPending = model.listOrderPending.Where(x => x.dateExport >= fromDate).ToList();
                }
                if (!string.IsNullOrEmpty(model.toDate))
                {
                    DateTime toDate = DateTime.ParseExact(model.fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    model.listOrderPending = model.listOrderPending.Where(x => x.dateExport <= toDate).ToList();
                }
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
           
            return View(model);
        }
    }
}