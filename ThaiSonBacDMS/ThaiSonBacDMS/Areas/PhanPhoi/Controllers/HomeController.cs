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
            HomeModel model = new HomeModel();
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var listTotal = totalDAO.getOrderByDateCreated(firstDayOfMonth); 
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
            var prodInMonth = productDAO.getProductByDateSold(firstDayOfMonth).Count;
            model.prodInMonth = prodInMonth;
            //return number of new customer
            var numberCustomer = customerDAO.getCustomerByDateCreated(firstDayOfMonth, lastDayOfMonth).Count;
            model.numberCustomer = numberCustomer;
            return View(model);
        }

        [ChildActionOnly]
        public PartialViewResult NotificationHeader()
        {
            var notiDAO = new NotificationDAO();
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            List<Notification> listNoti = new List<Notification>();
            listNoti = notiDAO.getByUserID(session.user_id);
            return PartialView(listNoti);
        }
        [ChildActionOnly]
        public PartialViewResult NoteEdit()
        {
            var noteDAO = new NoteDAO();
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            var content = noteDAO.getNotebyAccount(session.accountID);

            return PartialView(content);
        }

        [HttpPost]
        public JsonResult NoteEdit(int accID, string content)
        {
            var noteDAO = new NoteDAO();
            noteDAO.editNotebyAccount(accID, content);
            return Json(content, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public PartialViewResult NoteHeader()
        {
            var noteDAO = new NoteDAO();
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            var content = noteDAO.getNotebyAccount(session.accountID).Contents;
            string[] lines = new string[] { };
            if (content != null)
            {
                lines = content.Trim().Split(Environment.NewLine.ToCharArray());
            }
            else
            {
                lines = new string[] { "Hãy điền ghi chú vào đây" };
            }
            return PartialView(lines);
        }
    }
}