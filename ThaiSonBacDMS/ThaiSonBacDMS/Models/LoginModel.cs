using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Models
{
    public class LoginModel
    {
        public string accountName { get; set; }
        public string password { get; set; }
        public bool rememberMe { get; set; }


    }
}