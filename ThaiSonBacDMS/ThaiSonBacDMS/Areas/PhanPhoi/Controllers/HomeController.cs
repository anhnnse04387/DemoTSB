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
    public class HomeController : Controller
    {
        // GET: PhanPhoi/Home
        [HttpGet]
        public ActionResult Index()
        {
            //OrderTotalDAO totalDAO = new OrderTotalDAO();
            var firstDayOfTheMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //var listTotal = totalDAO.getOrderByDateCreated(firstDayOfTheMonth); 
            //return order in month
            //var orderInMonth = listTotal.Count;
            //return value in month
            decimal valueInMonth = 0;
            /*
            foreach(var item in listTotal)
            {
                valueInMonth += (decimal) item.Total_price;
            }
            */
            //return total product in month
            return View();
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
        public JsonResult NoteEdit(string accID, string content)
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
            if (!string.IsNullOrEmpty(content.Trim()))
            {
                lines = content.Split(Environment.NewLine.ToCharArray());
            }
            else
            {
                lines = new string[] { "Hãy điền ghi chú vào đây" };
            }
            return PartialView(lines);
        }
    }
}