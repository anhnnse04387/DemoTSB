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
    public class ChiTietNguoiDungController : Controller
    {
        // GET: QuanTri/ChiTietNguoiDung
        public ActionResult Index(int userId)
        {
            userId = 1;
            UserDAO userDao = new UserDAO();
            ChiTietNguoiDungModel model = new ChiTietNguoiDungModel();
            model.userInfor = new DanhSachNguoiDung();
            model.userInfor = userDao.getDetailUser(userId);
            return View(model);
        }
    }
}