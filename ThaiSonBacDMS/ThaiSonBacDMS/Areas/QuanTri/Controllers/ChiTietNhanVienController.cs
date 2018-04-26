using Models.DAO;
using Models.DAO_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.QuanTri.Models;

namespace ThaiSonBacDMS.Areas.QuanTri.Controllers
{
    public class ChiTietNhanVienController : QuanTriBaseController
    {
        // GET: QuanTri/ChiTietNguoiDung
        public ActionResult Index(int userId)
        {
            UserDAO userDao = new UserDAO();
            OfficeDAO officeDao = new OfficeDAO();
            RoleDetailDAO roleDao = new RoleDetailDAO();

            ChiTietNguoiDungModel model = new ChiTietNguoiDungModel();

            model.userInfor = new DanhSachNguoiDung();
            model.lstAllEmail = userDao.getAllEmail(userId);
            model.userInfor = userDao.getDetailUser(userId);
            model.roleId = model.userInfor.roleId;
            model.officeId = model.userInfor.officeId;
            model.lstOffice = new List<SelectListItem>();
            model.lstRole = new List<SelectListItem>();

            var lstAllRole = roleDao.lstAllRoleQuanTri();
            var lstAllOffice = officeDao.getListOffice();

            foreach (var item in lstAllRole)
            {
                model.lstRole.Add(new SelectListItem
                {
                    Text = @item.Role_name,
                    Value = @item.Role_ID.ToString()
                    //Selected=model.userInfor.chucVu
                });
            }

            foreach (var item in lstAllOffice)
            {
                model.lstOffice.Add(new SelectListItem
                {
                    Text = @item.Office_name,
                    Value = @item.Office_ID.ToString()
                });
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult checkExistedAcc(string account)
        {
            account = Common.RemoveVietNameseSign.RemoveSign(account);
            var result = "";
            UserDAO dao = new UserDAO();
            AccountDAO accDao = new AccountDAO();

            int maxId = accDao.getMaxId(account);
            if (maxId == 0)
            {
                result = account;
            }
            else
            {
                string accTemp = accDao.getExistingAcc(maxId);
                string c = accTemp.Substring(accTemp.Length - 1);
                if (!Char.IsNumber(Convert.ToChar(c)))
                {
                    result = account + "1";
                }
                else
                {
                    int param = Convert.ToInt32(c) + 1;
                    result = account + param;
                }
            }
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult UpdateData(string userId,string ten,string chucVu,string phanHe,string bhyt,string diaChi,string ngaySinh,string soDienThoai,string email,string isActive,string account)
        {
            UserDAO userDao = new UserDAO();
            AccountDAO accDao = new AccountDAO();

            userDao.updateUser(userId, ten, chucVu, phanHe, soDienThoai, email, bhyt, diaChi, ngaySinh, isActive);
            accDao.updateAccount(userId, account, chucVu, isActive);
             
            return Json(new { success = true, JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public JsonResult ResetPassWord(string userId)
        {
            string newPassWord = Common.Encryptor.MD5Hash("123@123");
            AccountDAO accDao = new AccountDAO();
            accDao.resetPassword(userId, newPassWord);
            return Json(new { success = true, JsonRequestBehavior.AllowGet });
        }
    }
}