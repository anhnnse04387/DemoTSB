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
<<<<<<< HEAD

            //setViewBag();
            return View();
=======
            var dao = new CustomerDAO();
            var model = new OrderTotalModel();
            model.lstCustomer = new SelectList(dao.getCustomer(), "Customer_ID", "Customer_name");
            return View(model);
>>>>>>> c5010b3600a2f6426226a542cb2e59792e0cec51
        }

        public void setViewBag(String selectedId = null)
        {
            var dao = new CustomerDAO();
            ViewBag.customerId = new SelectList(dao.getCustomer(), "Customer_ID", "Customer_name", selectedId);
        }
    }
}