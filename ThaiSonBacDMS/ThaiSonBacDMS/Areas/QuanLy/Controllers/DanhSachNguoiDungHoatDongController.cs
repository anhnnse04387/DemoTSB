using Models.DAO;
using Models.DAO_Model;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.QuanLy.Models;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers
{
    public class DanhSachNguoiDungHoatDongController : Controller
    {
        // GET: QuanLy/DanhSachNguoiDungHoatDong
        public ActionResult Index()
        {
            try
            {
                DanhSachNguoiDungHoatDongModel model = new DanhSachNguoiDungHoatDongModel();
                model.lstRole = new List<SelectListItem>();
                model.lstDisplay = new List<DanhSachNguoiDung>();
                UserDAO userDao = new UserDAO();
                RoleDetailDAO roleDao = new RoleDetailDAO();
                List<Role_detail> lstRole = new List<Role_detail>();
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                int userId = session.accountID;

                int currentRole = userDao.getRoleIdByCurrentAcc(userId.ToString());
                int roleQuanTri = userDao.getRoleQuanTri();
                int roleQuanLy = userDao.getRoleQuanTri();

                if (currentRole == roleQuanLy)
                {
                    model.lstDisplay = userDao.getAllUsersActiveByQuanLy();
                }


                lstRole = roleDao.lstAllRole();
                //khoi tao list role
                if (lstRole.Count() != 0)
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
    }
}
