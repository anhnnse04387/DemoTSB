using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Models
{
    public class RoleConfirmModel
    {
        public string account_name { get; set; }
        public byte roleID { get; set; }
        public IList<SelectListItem> account_role { get; set; }

    }
}