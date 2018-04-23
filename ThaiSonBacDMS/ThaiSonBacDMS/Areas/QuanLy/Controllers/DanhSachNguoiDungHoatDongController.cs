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
                AccountDAO accDao = new AccountDAO();
                RoleDetailDAO roleDao = new RoleDetailDAO();
                List<Role_detail> lstRole = new List<Role_detail>();
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                int accountId = session.accountID;

                int currentRole = accDao.getRoleIdByCurrentAcc(accountId.ToString());
                int roleQuanTri = userDao.getRoleQuanTri();
                int roleQuanLy = Convert.ToInt32(userDao.getRoleQuanLy());

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


        [HttpPost]
        public ActionResult Index(DanhSachNguoiDungHoatDongModel model)
        {
            try
            {
                model.lstRole = new List<SelectListItem>();

                UserDAO userDao = new UserDAO();
                AccountDAO accDao = new AccountDAO();
                RoleDetailDAO roleDao = new RoleDetailDAO();
                List<Role_detail> lstRole = new List<Role_detail>();
                var session = (UserSession)Session[CommonConstants.USER_SESSION];
                int accountId = session.accountID;

                int currentRole = accDao.getRoleIdByCurrentAcc(accountId.ToString());
                int roleQuanTri = userDao.getRoleQuanTri();
                int roleQuanLy = Convert.ToInt32(userDao.getRoleQuanLy());

                if (currentRole == roleQuanLy)
                {
                    model.lstDisplay = userDao.getAllUsersActiveByQuanLy(model.nameSearch, model.roleIdSearch, model.fromDate, model.toDate);
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


                //return Json(new { Data = model, JsonRequestBehavior.AllowGet });
                //return PartialView("_searchUsers",model);
                return View(model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }
        public JsonResult autoCompleteNameSearch(string searchValue)
        {
            UserDAO dao = new UserDAO();
            var lstAll = dao.getAllUsersActiveByQuanLy(searchValue);
            List<Autocomplete> lstSearch = lstAll.Select(x => new Autocomplete
            {
                key = x.tenNguoiDung,
            }).ToList();
            return new JsonResult { Data = lstSearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
    }
}
