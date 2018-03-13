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
        public ActionResult NoteEdit(Note note)
        {
            var noteDAO = new NoteDAO();
            noteDAO.editNotebyAccount(note.Account_ID, note.Contents);
            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public PartialViewResult NoteHeader()
        {
            var noteDAO = new NoteDAO();
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            var content = noteDAO.getNotebyAccount(session.accountID).Contents;
            string[] lines = content.Split(Environment.NewLine.ToCharArray());
            return PartialView(lines);
        }
    }
}