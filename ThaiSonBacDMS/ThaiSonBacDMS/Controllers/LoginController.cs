using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Models;
using ThaiSonBacDMS.Common;
using System.Web.Security;
using Models.Framework;

namespace ThaiSonBacDMS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            var chkSession = (UserSession)Session[CommonConstants.USER_SESSION];
            if(chkSession!=null)
            {
                switch (chkSession.roleSelectedID)
                {
                    case 1:
                        return RedirectToAction("Index", "Quantri/Home");
                    case 2:
                        return RedirectToAction("Index", "QuanLy/Home");
                    case 3:
                        return RedirectToAction("Index", "PhanPhoi/Home"); ;
                    case 4:
                        return RedirectToAction("Index", "HangHoa/Home");
                    case 5:
                        return RedirectToAction("Index", "KeToan/Home");
                    default:
                        break;
                }
            }
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
                    userSession.accountID = account.Account_ID;
                    userSession.user_name = currentUser.User_name;
                    userSession.user_id = currentUser.User_ID;
                    userSession.avatar_str = new MediaDAO().getMediaByID(int.Parse(currentUser.Avatar_ID)).Location;
                    if (roleDAO.getRoleByAccount(account.Account_ID).Count == 1)
                    {
                        var roleID = roleDAO.getRoleByAccount(account.Account_ID).SingleOrDefault().Role_ID;
                        var roleName = roleDAO.getByID(roleID).Role_name;
                        if (roleID == 1)
                        {
                            userSession.roleSelectedID = 1;
                            Session.Add(CommonConstants.USER_SESSION, userSession);
                            return RedirectToAction("Index", "Quantri/Home");
                        }
                        else if (roleID == 2)
                        {
                            userSession.roleSelectedID = 2;
                            Session.Add(CommonConstants.USER_SESSION, userSession);
                            return RedirectToAction("Index", "Quanly/Home");
                        }
                        else if (roleID == 3)
                        {
                            userSession.roleSelectedID = 3;
                            Session.Add(CommonConstants.USER_SESSION, userSession);
                            return RedirectToAction("Index", "PhanPhoi/Home");
                        }
                        else if (roleID == 4)
                        {
                            userSession.roleSelectedID = 4;
                            Session.Add(CommonConstants.USER_SESSION, userSession);
                            return RedirectToAction("Index", "HangHoa/Home");
                        }
                        else if (roleID == 5)
                        {
                            userSession.roleSelectedID = 5;
                            Session.Add(CommonConstants.USER_SESSION, userSession);
                            return RedirectToAction("Index", "KeToan/Home");
                        }
                        else if (roleID == 6)
                        {
                            userSession.roleSelectedID = 6;
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
                        userSession.accountID = account.Account_ID;
                        userSession.user_name = currentUser.User_name;
                        userSession.user_id = currentUser.User_ID;
                        userSession.avatar_str = new MediaDAO().getMediaByID(int.Parse(currentUser.Avatar_ID)).Location;

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
                                userSession.roleSelectedID = 1;
                                Session.Add(CommonConstants.USER_SESSION, userSession);
                                return RedirectToAction("Index", "Quantri/Home");
                            }
                            else if (roleID == 2)
                            {
                                userSession.roleSelectedID = 2;
                                Session.Add(CommonConstants.USER_SESSION, userSession);
                                return RedirectToAction("Index", "Quanly/Home");
                            }
                            else if (roleID == 3)
                            {
                                userSession.roleSelectedID = 3;
                                Session.Add(CommonConstants.USER_SESSION, userSession);
                                return RedirectToAction("Index", "PhanPhoi/Home");
                            }
                            else if (roleID == 4)
                            {
                                userSession.roleSelectedID = 4;
                                Session.Add(CommonConstants.USER_SESSION, userSession);
                                return RedirectToAction("Index", "HangHoa/Home");
                            }
                            else if (roleID == 5)
                            {
                                userSession.roleSelectedID = 5;
                                Session.Add(CommonConstants.USER_SESSION, userSession);
                                return RedirectToAction("Index", "KeToan/Home");
                            }
                            else if (roleID == 6)
                            {
                                userSession.roleSelectedID = 6;
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
            var session = (UserSession)Session[CommonConstants.USER_SESSION];
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
       
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(LoginModel model)
        {
            User u = new User();
            try
            {
                u = new UserDAO().getByAccountName(model.accountName);
                Notification noti = new Notification();
                noti.Content = "Tài khoản" + model.accountName + " muốn hoàn lại mật khẩu";
                noti.Notif_date = DateTime.Now;
                noti.Link = "/DanhSachNguoiDungDangHoatDong/ChiTiet?=" + u.User_ID;
                noti.User_ID = null;
                noti.Role_ID = 1;
                noti.Status = 1;
                new NotificationDAO().addNotification(noti);
                ModelState.AddModelError("", "Yêu cầu của bạn đã được gửi đi");
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại");
                System.Diagnostics.Debug.WriteLine(e);
            }
            return View();
        }
    }
}