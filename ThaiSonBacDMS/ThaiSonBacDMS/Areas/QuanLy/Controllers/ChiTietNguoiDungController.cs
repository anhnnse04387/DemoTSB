using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.QuanLy.Models;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers
{
    public class ChiTietNguoiDungController : QuanLyBaseController
    {
        // GET: QuanLy/ChiTietNguoiDung
        public ActionResult Index(int userId)
        {
            ChiTietNguoiDungModel model = new ChiTietNguoiDungModel();
            RoleDetailDAO roleDao = new RoleDetailDAO();
            UserDAO userDao = new UserDAO();
            OfficeDAO officeDao = new OfficeDAO();

            model.userInfor = userDao.getDetailUser(userId);
            model.roleId = model.userInfor.roleId;
            model.officeId = model.userInfor.officeId;
            model.lstRole = new List<SelectListItem>();
            model.lstOffice = new List<SelectListItem>();

            var lstTemp = roleDao.lstAllRole();
            foreach(var item in lstTemp)
            {
                model.lstRole.Add(new SelectListItem
                {
                    Text = item.Role_name,
                    Value = item.Role_ID.ToString()
                });
            }
            var lstOffice = officeDao.getListBySelectedValue(model.roleId);
            foreach(var item in lstOffice)
            {
                model.lstOffice.Add(new SelectListItem
                {
                    Text = item.key,
                    Value = item.value.ToString()
                });
            }


            return View(model);
        }
    }
}