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
        public ActionResult Index()
        {
            //set username and password if has cookie
            string username = string.Empty;
            string password = string.Empty;
            if (Request.Cookies["username"] != null)
                username = Request.Cookies["username"].Value;
            if (Request.Cookies["password"] != null)
                password = Request.Cookies["password"].Value;

            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
                return View();
            else
            {
                var accDAO = new AccountDAO();
                var roleDAO = new RoleDAO();
                var userDAO = new UserDAO();
                var encryptor = Encryptor.MD5Hash(password);
                var result = accDAO.Login(username, encryptor);
                if (result == 1)
                {
                    var account = accDAO.GetByName(username);
                    var currentUser = userDAO.getByAccountID(account.Account_ID);
                    var userSession = new UserSession();
                    userSession.role_name = new UserDAO().getOffice((byte)currentUser.Office_ID);
                    userSession.account_name = account.Account_name;
                    userSession.accountID = account.Account_ID;
                    userSession.user_name = currentUser.User_name;
                    userSession.user_id = currentUser.User_ID;

                    if (roleDAO.getRoleByAccount(account.Account_ID).Count == 1)
                    {
                        var roleID = roleDAO.getRoleByAccount(account.Account_ID).SingleOrDefault().Role_ID;
                        var roleName = roleDAO.getByID(roleID).Role_name;
                        if (roleID == 1)
                        {
                            Session.Add(CommonConstants.USER_SESSION, userSession);
                            return RedirectToAction("Index", "Quantri/Home");
                        }
                        else if (roleID == 2)
                        {
                            Session.Add(CommonConstants.USER_SESSION, userSession);
                            return RedirectToAction("Index", "Quanly/Home");
                        }
                        else if (roleID == 3)
                        {
                            Session.Add(CommonConstants.USER_SESSION, userSession);
                            return RedirectToAction("Index", "PhanPhoi/Home");
                        }
                        else if (roleID == 4)
                        {
                            Session.Add(CommonConstants.USER_SESSION, userSession);
                            return RedirectToAction("Index", "HangHoa/Home");
                        }
                        else if (roleID == 5)
                        {
                            Session.Add(CommonConstants.USER_SESSION, userSession);
                            return RedirectToAction("Index", "KeToan/Home");
                        }
                        else if (roleID == 6)
                        {
                            Session.Add(CommonConstants.USER_SESSION, userSession);
                            return RedirectToAction("Index", "GiaoHang/Home");
                        }
                    }
                    else
                    {
                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        return RedirectToAction("ConfirmRole", "ConfirmRole");
                    }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var accDAO = new AccountDAO();
                    var roleDAO = new RoleDAO();
                    var userDAO = new UserDAO();
                    var encryptor = Encryptor.MD5Hash(model.password);
                    if(string.IsNullOrEmpty(model.accountName) || string.IsNullOrEmpty(encryptor))
                    {
                        ModelState.AddModelError("", "Xin hãy điền đầy đủ tài khoản và mật khẩu");
                        return View();
                    }
                    var result = accDAO.Login(model.accountName, encryptor);
                    if (result == 1)
                    {
                        var account = accDAO.GetByName(model.accountName);
                        var currentUser = userDAO.getByAccountID(account.Account_ID);
                        var userSession = new UserSession();
                        userSession.user_info = currentUser;
                        userSession.role_name = new UserDAO().getOffice((byte) currentUser.Office_ID);
                        userSession.roleSelectFlag = false;
                        userSession.account_name = account.Account_name;
                        userSession.accountID = account.Account_ID;
                        userSession.user_name = currentUser.User_name;
                        userSession.user_id = currentUser.User_ID;

                        //set cookie
                        if (model.rememberMe)
                        {
                            //set cookie username
                            HttpCookie ckUsername = new HttpCookie("username");
                            ckUsername.Expires = DateTime.Now.AddSeconds(3600);
                            ckUsername.Value = model.accountName;
                            Response.Cookies.Add(ckUsername);
                            //set cookie password
                            HttpCookie ckPassword = new HttpCookie("password");
                            ckPassword.Expires = DateTime.Now.AddSeconds(3600);
                            ckPassword.Value = model.password;
                            Response.Cookies.Add(ckPassword);
                        }
                        if (roleDAO.getRoleByAccount(account.Account_ID).Count == 1)
                        {
                            var roleID = roleDAO.getRoleByAccount(account.Account_ID).SingleOrDefault().Role_ID;
                            var roleName = roleDAO.getByID(roleID).Role_name;
                            if (roleID == 1)
                            {
                                Session.Add(CommonConstants.USER_SESSION, userSession);
                                return RedirectToAction("Index", "Quantri/Home");
                            }
                            else if (roleID == 2)
                            {
                                Session.Add(CommonConstants.USER_SESSION, userSession);
                                return RedirectToAction("Index", "Quanly/Home");
                            }
                            else if (roleID == 3)
                            {
                                Session.Add(CommonConstants.USER_SESSION, userSession);
                                return RedirectToAction("Index", "PhanPhoi/Home");
                            }
                            else if (roleID == 4)
                            {
                                Session.Add(CommonConstants.USER_SESSION, userSession);
                                return RedirectToAction("Index", "HangHoa/Home");
                            }
                            else if (roleID == 5)
                            {
                                Session.Add(CommonConstants.USER_SESSION, userSession);
                                return RedirectToAction("Index", "KeToan/Home");
                            }
                            else if (roleID == 5)
                            {
                                Session.Add(CommonConstants.USER_SESSION, userSession);
                                return RedirectToAction("Index", "GiaoHang/Home");
                            }
                        }
                        else
                        {
                            userSession.roleSelectFlag = true;
                            Session.Add(CommonConstants.USER_SESSION, userSession);
                            return RedirectToAction("ConfirmRole", "ConfirmRole");
                        }


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
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                ModelState.AddModelError("", "Đã có lỗi xảy ra khi đăng nhập");
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            //clear cookie
            if (Response.Cookies["username"] != null)
            {
                HttpCookie ckUsername = new HttpCookie("username");
                ckUsername.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(ckUsername);
            }
            if (Response.Cookies["password"] != null)
            {
                HttpCookie ckPassword = new HttpCookie("password");
                ckPassword.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(ckPassword);
            }
            return RedirectToAction("Index", "Login");
        }

    }
}