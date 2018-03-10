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
    public class HomeController : Controller
    {
        // GET: PhanPhoi/Home
        [HttpGet]
        public ActionResult Index()
        {
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
    }
}