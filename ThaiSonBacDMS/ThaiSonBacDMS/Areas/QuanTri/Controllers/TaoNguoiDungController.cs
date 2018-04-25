using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.QuanTri.Models;
using ThaiSonBacDMS.Common;
using Models.Framework;
using System.Globalization;

namespace ThaiSonBacDMS.Areas.QuanTri.Controllers
{
    public class TaoNguoiDungController : Controller
    {
        // GET: QuanTri/TaoNguoiDung
        public ActionResult Index()
        {
            try
            {
                RoleDetailDAO roleDao = new RoleDetailDAO();
                OfficeDAO officeDao = new OfficeDAO();
                RoleDetailDAO roleDeDao = new RoleDetailDAO();
                UserDAO userDao = new UserDAO();

                TaoNguoiDungModel model = new TaoNguoiDungModel();

                model.lstEmail = userDao.getAllEmail();
                model.lstOffice = new List<SelectListItem>();
                model.lstRole = new List<SelectListItem>();

                var lstOffice = officeDao.getListOffice();
                var lstRole = roleDeDao.lstAllRole();

                if (lstRole.Count() != 0)
                {
                    foreach (var item in lstOffice)
                    {
                        model.lstOffice.Add(new SelectListItem { Text = item.Office_name, Value = item.Office_ID.ToString() });
                    }
                }
                if (lstRole.Count != 0)
                {
                    foreach (var item in lstRole)
                    {
                        model.lstRole.Add(new SelectListItem { Text = item.Role_name, Value = item.Role_ID.ToString() });
                    }
                }
                return View(model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            try
            {
                UserDAO userDao = new UserDAO();
                MediaDAO mediaDao = new MediaDAO();
                AccountDAO accDao = new AccountDAO();

                if (Request.Files.Count > 0)
                {
                    var session = (UserSession)Session[CommonConstants.USER_SESSION];
                    var userId = session.user_id;
                    var name = Request.Form.GetValues("name")[0];
                    var dob = Request.Form.GetValues("dob")[0];
                    var office = Request.Form.GetValues("office")[0];
                    var email = Request.Form.GetValues("email")[0];
                    var address = Request.Form.GetValues("address")[0];
                    var insuranceNo = Request.Form.GetValues("insuranceNo")[0];
                    var role = Request.Form.GetValues("role")[0];
                    var phoneNumber = Request.Form.GetValues("phoneNumber")[0];
                    var nameWithoutSign = Common.RemoveVietNameseSign.RemoveSign(name);
                    var st = nameWithoutSign.Trim().Split(' ');
                    var account = st[st.Length - 1];
                    for (var i = 0; i < st.Length - 1; i++)
                    {
                        account += st[i].ToCharArray()[0];
                    }
                    int isExsit = accDao.checkExistAcc(account);
                    if (isExsit != 0)
                    {
                        int accountId = accDao.getMaxId(account);
                        string existedAcc = accDao.getExistingAcc(accountId);
                        string c = existedAcc.Substring(existedAcc.Length - 1);
                        if (!Char.IsNumber(Convert.ToChar(c)))
                        {
                            account += "1";
                        }
                        else
                        {
                            account += Convert.ToInt32(c) + 1;
                        }
                    }

                    var isActive = Request.Form.GetValues("active")[0];

                    User user = new User();
                    user.User_name = name;
                    user.Office_ID = Convert.ToByte(office);
                    user.Role_ID = Convert.ToByte(role);
                    user.User_Address = address;
                    user.Phone = phoneNumber;
                    user.Date_of_birth = DateTime.ParseExact(dob, "d-M-yyyy", CultureInfo.InvariantCulture);
                    user.Insurance_Code = insuranceNo;
                    user.Mail = email;

                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        // Get the complete folder path and store the file inside it. 
                        string newFname = fname;
                        fname = Path.Combine(Server.MapPath("~/Assets/dist/img/Resource"), fname);
                        string path = "/Assets/dist/img/Resource/" + file.FileName;
                        file.SaveAs(fname);

                        int mediaId = mediaDao.insertMedia("User", path, userId);
                        int userID = userDao.insertNewUser(user, mediaId.ToString());
                        string passWord = Common.Encryptor.MD5Hash("123@123");
                        accDao.insertNewAccount(account, passWord, userID, role, isActive);
                    }

                }
                return Json(new { success = true, JsonRequestBehavior.AllowGet });
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public JsonResult checkExsitedMail(string email)
        {
            UserDAO dao = new UserDAO();
            int result = dao.checkExsitedEmail(email);
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}