﻿using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Common;
using ThaiSonBacDMS.Controllers;

namespace ThaiSonBacDMS.Areas.HangHoa.Controllers
{
    public class HomeController : HangHoaBaseController
    {
        // GET: HangHoa/Home
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
    }
}