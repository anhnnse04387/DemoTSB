using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class LapPhieuController : Controller
    {
        // GET: PhanPhoi/LapPhieu
        public ActionResult Index()
        {
            var dao = new CustomerDAO();
            var model = new OrderTotalModel();
            model.lstCustomer = new SelectList(dao.getCustomer(), "Customer_ID", "Customer_name");
            model.deliveryQtt = 1;
            model.rate = 2016;
            return View(model);
        }
    }
}