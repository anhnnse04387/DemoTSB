using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class LapPhieuController : Controller
    {
        // GET: PhanPhoi/LapPhieu
        public ActionResult Index()
        {

            //setViewBag();
            return View();
        }

        public void setViewBag(String selectedId = null)
        {
            var dao = new CustomerDAO();
            ViewBag.customerId = new SelectList(dao.getCustomer(), "Customer_ID", "Customer_name", selectedId);
        }
    }
}