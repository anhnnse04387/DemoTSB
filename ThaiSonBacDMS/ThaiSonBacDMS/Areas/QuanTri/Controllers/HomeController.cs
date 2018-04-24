using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.QuanTri.Models;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.QuanTri.Controllers
{
    public class HomeController : QuanTriBaseController
    {
        // GET: QuanTri/Home
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            try
            {
                model.accountCount = new AccountDAO().accountCount();
                model.categoryCount = new CategoryDAO().categoryCount();
                model.roleCount = new RoleDAO().roleCount();
                model.pageCount = 0;
                model.productCount = new ProductDAO().productCount();
                model.fileCount = new MediaDAO().mediaCount();
            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
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
        [ChildActionOnly]
        public PartialViewResult NoteEdit()
        {
            try
            {
                var noteDAO = new NoteDAO();
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                var content = noteDAO.getNotebyAccount(session.accountID);

                return PartialView(content);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                RedirectToAction("Index");
            }
            return PartialView();
        }

        [HttpPost]
        public JsonResult NoteEdit(int accID, string content)
        {
            try
            {
                var noteDAO = new NoteDAO();
                noteDAO.editNotebyAccount(accID, content);
                return Json(content, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                RedirectToAction("Index");
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public PartialViewResult NoteHeader()
        {
            var noteDAO = new NoteDAO();
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            var content = String.Empty;
            string[] lines = new string[] { };
            try
            {
                content = noteDAO.getNotebyAccount(session.accountID).Contents;
                if (content != null && !string.IsNullOrEmpty(content))
                {

                    lines = content.Trim().Split(Environment.NewLine.ToCharArray());
                }
                else
                {
                    lines = new string[] { "Hãy điền ghi chú vào đây" };
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                RedirectToAction("Index");
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