using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Models;
using ThaiSonBacDMS.Common;
using System.Web.Security;

namespace ThaiSonBacDMS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var accDAO = new AccountDAO();
                var roleDAO = new RoleDAO();
                //var encryptor = Encryptor.MD5Hash(model.password);
                var result = accDAO.Login(model.accountName, model.password);
                if (result == 1)
                {
                    var account = accDAO.GetByName(model.accountName);
                    var userSession = new UserSession();
                    userSession.account_name = account.Account_name;
                    userSession.accountID = account.Account_ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);

                    return RedirectToAction("ConfirmRole", "ConfirmRole");                    
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đã bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login", "Login");
        }


    }
}