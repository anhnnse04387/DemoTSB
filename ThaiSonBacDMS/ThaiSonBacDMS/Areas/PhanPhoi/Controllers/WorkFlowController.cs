using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Controllers;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers
{
    public class WorkFlowController : PhanPhoiBaseController
    {
        // GET: PhanPhoi/WorkFlow
        public ActionResult Index()
        {
            ViewBag.LinkImage = "/Assets/dist/img/Resource/workflow/Workflow.jpg";
            return View();
        }
    }
}