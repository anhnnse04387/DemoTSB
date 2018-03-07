using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThaiSonBacDMS.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Xin hãy nhập tài khoản")]
        public string accountName { get; set; }
        [Required(ErrorMessage = "Xin hãy nhập mật khẩu")]
        public string password { get; set; }
        public bool rememberMe { get; set; }


    }
}