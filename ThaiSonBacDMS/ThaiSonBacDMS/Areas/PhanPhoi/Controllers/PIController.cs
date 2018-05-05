using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class PIController : PhanPhoiBaseController
    {
        // GET: PhanPhoi/PI
        public ActionResult Index()
        {
            var model = new PIModel();
            var ddlPO = new List<SelectListItem>();            
            var dao = new PODAO();
            var lstPO = dao.getLstPO();
            lstPO.ForEach(x =>
            {
                ddlPO.Add(new SelectListItem { Text = x.PO_no, Value = x.PO_ID.ToString() });
            });
            model.lstPO = ddlPO;
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var dao = new PIDAO();
            var model = new PIModel();
            var daoProduct = new ProductDAO();
            var data = dao.getPI(id);
            model.pi = data;
            var readItems = new List<PIItemModel>();
            var ddlPO = new List<SelectListItem>();
            var poDAO = new PODAO();
            var lstPO = poDAO.getLstPO();
            lstPO.ForEach(x =>
            {
                ddlPO.Add(new SelectListItem { Text = x.PO_no, Value = x.PO_ID.ToString() });
            });
            model.lstPO = ddlPO;
            foreach (Purchase_invoice_Items i in data.Purchase_invoice_Items)
            {
                var product = daoProduct.getProductById(i.Product_ID);
                var item = new PIItemModel {
                    product = product.Product_name,
                    NOTE = i.NOTE,
                    per = product.CIF_USD,
                    Price = i.Price,
                    Quantity = i.Quantity
                };
                readItems.Add(item);
            }
            model.readItems = readItems;
            return View(model);
        }

        public ActionResult ChooseNo(int no)
        {
            try
            {
                var model = new PIModel();
                var daoPO = new PODAO();
                var daoProduct = new ProductDAO();
                var lstItem = new List<PIItemModel>();
                var po = daoPO.getPO(no);
                var pi = new Purchase_invoice();
                pi.Supplier_ID = po.Supplier_ID;
                pi.Total_price = po.Total_price;
                int? total = 0;
                foreach (PO_Items p in po.PO_Items)
                {
                    var product = daoProduct.getProductById(p.Product_ID);
                    var item = new PIItemModel
                    {
                        Product_ID = p.Product_ID,
                        product = product.Product_name,
                        NOTE = p.NOTE,
                        Quantity = p.Quantity,
                        per = product.CIF_USD,
                        Price = p.Price
                    };
                    total += p.Quantity;
                    lstItem.Add(item);
                }
                model.qttAll = total;
                model.readItems = lstItem;
                model.pi = pi;
                return PartialView("_noPartial", model);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult CheckOut(PIModel model)
        {
            try
            {
                if (!String.IsNullOrEmpty(model.pi.Commodity) && !String.IsNullOrEmpty(model.pi.Country_of_origin) &&
                    !String.IsNullOrEmpty(model.pi.Date_requested.ToString()) && !String.IsNullOrEmpty(model.pi.Final_destination) &&
                    !String.IsNullOrEmpty(model.pi.Inspection) && !String.IsNullOrEmpty(model.pi.Packing) &&
                    !String.IsNullOrEmpty(model.pi.Payment_term) && !String.IsNullOrEmpty(model.pi.Post_of_Loading) &&
                    !String.IsNullOrEmpty(model.pi.Price_term) && !String.IsNullOrEmpty(model.pi.Purchase_invoice_no) &&
                    !String.IsNullOrEmpty(model.pi.Remarks) && !String.IsNullOrEmpty(model.pi.Shipment_date.ToString()) &&
                    !String.IsNullOrEmpty(model.pi.Validity) && model.pi.Supplier_ID > 0 && model.pi.PO_ID > 0 &&
                    model.pi.Total_price > 0)
                {
                    var session = (UserSession)Session[CommonConstants.USER_SESSION];
                    var dao = new PIDAO();
                    model.pi.Purchase_invoice_Items = model.items;
                    dao.createPI(model.pi);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}