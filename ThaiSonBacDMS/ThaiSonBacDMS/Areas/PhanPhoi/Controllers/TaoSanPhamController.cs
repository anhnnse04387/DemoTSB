using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;
using ThaiSonBacDMS.Controllers;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class TaoSanPhamController : BaseController
    {
        // GET: PhanPhoi/TaoSanPham
        public ActionResult Index(TaoSanPhamModel model)
        {
            var file = model.file;
            var file2 = model.file;
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            foreach (string item in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                string fileName = file.FileName;
                string UploadPath = "~/Assets/dist/";

                if (file.ContentLength == 0)
                    continue;
                if (file.ContentLength > 0)
                {
                    string path = Path.Combine(HttpContext.Request.MapPath(UploadPath), fileName);
                    string extension = Path.GetExtension(file.FileName);

                    file.SaveAs(path);
                }
            }

            return Json("");
        }
    }
}