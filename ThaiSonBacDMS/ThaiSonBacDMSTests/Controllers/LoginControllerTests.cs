using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThaiSonBacDMS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThaiSonBacDMS.Models;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Controllers.Tests
{
    [TestClass()]
    public class LoginControllerTests
    {
        //Test account and password null
        [TestMethod()]
        public void LoginTestAccountNull()
        {
            var controler = new LoginController();
            var model = new LoginModel();
            var result = controler.Index(model) as ViewResult;
            var returnMsg = result.ViewData.ModelState.Values.First().Errors.First().ErrorMessage;
            Assert.AreEqual("Đã có lỗi xảy ra khi đăng nhập", returnMsg);
        }

        //Test account empty and password null 
        [TestMethod()]
        public void LoginTestAccountEmpty()
        {
            var controler = new LoginController();
            var model = new LoginModel();
            model.accountName = string.Empty;

            var result = controler.Index(model) as ViewResult;
            var returnMsg = result.ViewData.ModelState.Values.First().Errors.First().ErrorMessage;
            Assert.AreEqual("Đã có lỗi xảy ra khi đăng nhập", returnMsg);
        }

        //Test account null and password empty  
        [TestMethod()]
        public void LoginTestPasswordEmpty()
        {
            var controler = new LoginController();
            var model = new LoginModel();
            model.password = string.Empty;

            var result = controler.Index(model) as ViewResult;
            var returnMsg = result.ViewData.ModelState.Values.First().Errors.First().ErrorMessage;
            Assert.AreEqual("Xin hãy điền đầy đủ tài khoản và mật khẩu", returnMsg);
        }
    }
}