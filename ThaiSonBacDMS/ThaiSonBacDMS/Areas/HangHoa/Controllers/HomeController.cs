﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThaiSonBacDMS.Controllers;

namespace ThaiSonBacDMS.Areas.HangHoa.Controllers
{
    public class HomeController : BaseController
    {
        // GET: HangHoa/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}