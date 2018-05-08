using Models.DAO;
using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class ChiTietSanPhamController : PhanPhoiBaseController
    {
        // GET: PhanPhoi/ChiTietSanPham
        public ActionResult Index(int product_Id)
        {
            ProductDAO daoProduct = new ProductDAO();
            OrderItemDAO daoOrderItem = new OrderItemDAO();
            MediaDAO daoMedia = new MediaDAO();
            ChiTietSanPhamModel model = new ChiTietSanPhamModel();
            var lstMedia= daoMedia.getMediaId(product_Id);
            model.lstLocation = new List<string>();
            if(lstMedia.Count() > 0)
            {
                foreach(var item in lstMedia)
                {
                    string location = daoMedia.getProductMedia(Convert.ToInt32(item));
                    model.lstLocation.Add(location);
                }
            }
           
            model.lstDisplay = new List<ShowOrderItemModel>();

            List<Order_items> lstTemp = new List<Order_items>();
            lstTemp = daoOrderItem.getLstOrderItems(product_Id);

            if (lstTemp.Count != 0)
            {
               foreach(Order_items item in lstTemp)
                {
                    ShowOrderItemModel soim = new ShowOrderItemModel();
                    soim.item = item;
                    model.lstDisplay.Add(soim);
                }
            }
            model.itemDisplay.product = new Product();
            model.itemDisplay.product = daoProduct.getProductByProductId(product_Id);
            
            return View(model);
        }
    }
}