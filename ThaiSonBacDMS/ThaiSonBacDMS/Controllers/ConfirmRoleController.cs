using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Common;
using ThaiSonBacDMS.Models;

namespace ThaiSonBacDMS.Controllers
{
    public class ConfirmRoleController : BaseController
    {
        [HttpGet]
        // GET: ConfirmRole
        public ActionResult ConfirmRole()
        {
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            var roleDAO = new RoleDAO();
            var userDAO = new UserDAO();

            var currentUser = userDAO.getByAccountID(session.accountID);
            var listRole = roleDAO.getRoleByAccount(session.accountID);
            

            var display = new RoleConfirmModel();
            display.account_name = currentUser.User_name;
            display.account_role = new List<SelectListItem>();
            foreach (var i in listRole)
            {
                var item = new SelectListItem { Text = i.Role_name, Value = i.Role_ID.ToString() };
                display.account_role.Add(item);
            }
            return View(display);
        }
        [HttpPost]
        public ActionResult ConfirmRole(RoleConfirmModel model)
        {
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
            var roleDAO = new RoleDAO();
            var userDAO = new UserDAO();

            var currentUser = userDAO.getByAccountID(session.accountID);
            var listRole = roleDAO.getRoleByAccount(session.accountID);
            var userSession = new UserSession();
            userSession.account_name = session.account_name;
            userSession.accountID = session.accountID;
            userSession.user_name = currentUser.User_name;
            userSession.user_id = currentUser.User_ID;
            var roleID = model.roleID;
            var roleName = roleDAO.getByID(roleID).Role_name;
            if (roleID == 1)
            {
                userSession.role_name = roleName;
                Session.Add(CommonConstants.USER_SESSION, userSession);
                return RedirectToAction("Index", "Quantri/Home");
            }
            else if (roleID == 2)
            {
                userSession.role_name = roleName;
                Session.Add(CommonConstants.USER_SESSION, userSession);
                return RedirectToAction("Index", "Quanly/Home");
            }
            else if (roleID == 3)
            {
                userSession.role_name = roleName;
                Session.Add(CommonConstants.USER_SESSION, userSession);
                return RedirectToAction("Index", "PhanPhoi/Home");
            }
            else if (roleID == 4)
            {
                userSession.role_name = roleName;
                Session.Add(CommonConstants.USER_SESSION, userSession);
                return RedirectToAction("Index", "HangHoa/Home");
            }
            else if (roleID == 5)
            {
                userSession.role_name = roleName;
                Session.Add(CommonConstants.USER_SESSION, userSession);
                return RedirectToAction("Index", "KeToan/Home");
            }
            else if (roleID == 5)
            {
                userSession.role_name = roleName;
                Session.Add(CommonConstants.USER_SESSION, userSession);
                return RedirectToAction("Index", "GiaoHang/Home");
            }
            
            return View();
        }
    }
}