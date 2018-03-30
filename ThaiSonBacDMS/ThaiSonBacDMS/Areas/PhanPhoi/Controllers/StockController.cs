using Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class StockController : Controller
    {
        // GET: PhanPhoi/Stock
        public ActionResult Index(StockModel model)
        {
            var ddl = new List<SelectListItem>();
            if(model == null)
            {
                model = new StockModel();
            }
            var dao = new PIDAO();
            var lstNo = dao.getLstPI();
            lstNo.ForEach(x =>
            {
                ddl.Add(new SelectListItem { Text = x.Purchase_invoice_no, Value = x.Purchase_invoice_ID.ToString() });
            });
            model.lstNo = ddl;
            model.status = true;
            return View(model);
        }

        public ActionResult ChangeStatus(bool status)
        {
            var ddl = new List<SelectListItem>();
            var model = new StockModel();
            var daoPI = new PIDAO();
            var daoOrder = new OrderTotalDAO();
            if (status)
            {
                var lstPI = daoPI.getLstPI();
                lstPI.ForEach(x =>
                {
                    ddl.Add(new SelectListItem { Text = x.Purchase_invoice_no, Value = x.Purchase_invoice_ID.ToString() });
                });
                model.lstNo = ddl;
                model.status = true;
            }
            else
            {
                var lstOrder = daoOrder.getLstOrder();
                lstOrder.ForEach(x =>
                {
                    ddl.Add(new SelectListItem { Text = x.Order_ID, Value = x.Order_ID });
                });
                model.lstNo = ddl;
                model.status = false;
            }
            return Index(model);
        }

        public ActionResult ChooseNo(String no)
        {
            return View();
        }
    }
}