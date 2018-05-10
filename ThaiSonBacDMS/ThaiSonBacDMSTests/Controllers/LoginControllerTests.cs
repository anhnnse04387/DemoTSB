using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThaiSonBacDMS.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThaiSonBacDMS.Models;
using System.Web.Mvc;
using Moq;
using System.Web;
using System.Web.Routing;
using Models.Framework;
using Models.DAO;

namespace ThaiSonBacDMS.Controllers.Tests
{
    [TestClass()]
    public class LoginControllerTests
    {
        /*
         * Create sample data
         **/
         public RequestContext setContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();

            response.Setup(r => r.Cookies).Returns(new HttpCookieCollection());
            request.SetupGet(r => r.Cookies).Returns(new HttpCookieCollection());
            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);

            RequestContext rc = new RequestContext(context.Object, new RouteData());
            return rc;
        }
        //create sample account
        public User sampleUser()
        {
            User sampleUser = new User();
            sampleUser.User_name = "Nguyễn Mạnh Cường";
            sampleUser.Status = 1;
            sampleUser.User_ID = 100;
            return sampleUser;
        }
        public Account sampleData()
        {
            Account sample = new Account();
            sample.Account_name = "CuongNM21";
            sample.Password = "123456";
            sample.User_ID = sampleUser().User_ID; 
            return sample;
        }
        /*
         * Test for login view
         **/

        //Test view login page with cookie null
        [TestMethod()]
        public void LoginTest_CookieNull()
        {
            var controler = new LoginController();
            controler.ControllerContext = new ControllerContext(setContext(), controler);
            var model = new LoginModel();
            var result = controler.Index() as ViewResult;            
            Assert.AreEqual(result.ViewEngineCollection.Count, 2);
        }

        //Test account and password null
        [TestMethod()]
        public void LoginTest_AccountNullPasswordNull()
        {
            var controler = new LoginController();
            var model = new LoginModel();
            var result = controler.Index(model) as ViewResult;
            var returnMsg = result.ViewData.ModelState.Values.First().Errors.First().ErrorMessage;
            Assert.AreEqual("Đã có lỗi xảy ra khi đăng nhập", returnMsg);
        }

        //Test account empty and password null 
        [TestMethod()]
        public void LoginTest_AccountEmptyPasswordNull()
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
        public void LoginTest_AccountNullPasswordEmpty()
        {
            var controler = new LoginController();
            var model = new LoginModel();
            model.password = string.Empty;
            var result = controler.Index(model) as ViewResult;
            var returnMsg = result.ViewData.ModelState.Values.First().Errors.First().ErrorMessage;
            Assert.AreEqual("Xin hãy điền đầy đủ tài khoản và mật khẩu", returnMsg);
        }

        //Test account out of length
        [TestMethod()]
        public void LoginTest_AccountOutLength()
        {
            var controler = new LoginController();
            var model = new LoginModel();
            model.accountName = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz";
            model.password = string.Empty;

            var result = controler.Index(model) as ViewResult;
            var returnMsg = result.ViewData.ModelState.Values.First().Errors.First().ErrorMessage;
            Assert.AreEqual("Tài khoản và mật khẩu < 20 ký tự", returnMsg);
        }

        //Test password out of length  
        [TestMethod()]
        public void LoginTest_PasswordOutLength()
        {
            var controler = new LoginController();
            var model = new LoginModel();
            model.accountName = "abvdas";
            model.password = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz";

            var result = controler.Index(model) as ViewResult;
            var returnMsg = result.ViewData.ModelState.Values.First().Errors.First().ErrorMessage;
            Assert.AreEqual("Tài khoản và mật khẩu < 20 ký tự", returnMsg);
        }

        //Test account special character
        [TestMethod()]
        public void LoginTest_AccountSpecialCharacter()
        {
            var controler = new LoginController();
            var model = new LoginModel();
            model.accountName = "@zxcz@#$";
            model.password = string.Empty;

            var result = controler.Index(model) as ViewResult;
            var returnMsg = result.ViewData.ModelState.Values.First().Errors.First().ErrorMessage;
            Assert.AreEqual("Tài khoản không tồn tại", returnMsg);
        }

        //Test account not exists
        [TestMethod()]
        public void LoginTest_AccountNotExists()
        {
            var controler = new LoginController();
            var model = new LoginModel();
            model.accountName = "CuongNM123456789";
            model.password = "12344566";

            var result = controler.Index(model) as ViewResult;
            var returnMsg = result.ViewData.ModelState.Values.First().Errors.First().ErrorMessage;
            Assert.AreEqual("Tài khoản không tồn tại", returnMsg);
        }

        //Test login wrong password
        [TestMethod()]
        public void LoginTest_WrongPassword()
        {
            var controler = new LoginController();
            var model = new LoginModel();
            model.accountName = "BanTQ";
            model.password = "12344565";

            var result = controler.Index(model) as ViewResult;
            var returnMsg = result.ViewData.ModelState.Values.First().Errors.First().ErrorMessage;
            Assert.AreEqual("Mật khẩu không đúng", returnMsg);
        }

        //Test login account lock
        [TestMethod()]
        public void LoginTest_AccountLock()
        {
            var controler = new LoginController();
            var model = new LoginModel();
            model.accountName = "DongD";
            model.password = "Dong123";
            var result = controler.Index(model) as ViewResult;
            var returnMsg = result.ViewData.ModelState.Values.First().Errors.First().ErrorMessage;
            Assert.AreEqual("Tài khoản đã bị khóa", returnMsg);
        }

        //Test login succesfully
        [TestMethod()]
        public void LoginTest_LoginSuccesfullySalesRole()
        {
            var controler = new LoginController();
            controler.ControllerContext = new ControllerContext(setContext(), controler);
            var model = new LoginModel();
            model.accountName = "DatLM";
            model.password = "Dat123";
            var result = controler.Index(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("PhanPhoi/Home", result.RouteValues["controller"]);
        }

        //test login accountant
        [TestMethod()]
        public void LoginTest_LoginSuccesfullyAccountantRole()
        {
            var controler = new LoginController();
            controler.ControllerContext = new ControllerContext(setContext(), controler);
            var model = new LoginModel();
            model.accountName = "MinhTA";
            model.password = "Minh123";
            var result = controler.Index(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("KeToan/Home", result.RouteValues["controller"]);
        }

        //test login warehouse
        [TestMethod()]
        public void LoginTest_LoginSuccesfullyWarehouseRole()
        {
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            var controler = new LoginController();
            controler.ControllerContext = context.Object;
            var model = new LoginModel();
            model.accountName = "SonNT";
            model.password = "Son123";
            var result = controler.Index(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("HangHoa/Home", result.RouteValues["controller"]);
        }

        //test login admin
        [TestMethod()]
        public void LoginTest_LoginSuccesfullyAdministratorRole()
        {
            var controler = new LoginController();
            controler.ControllerContext = new ControllerContext(setContext(), controler);
            var model = new LoginModel();
            model.accountName = "SonNT";
            model.password = "Son123";
            var result = controler.Index(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("HangHoa/Home", result.RouteValues["controller"]);
        }

        //test login manager
        [TestMethod()]
        public void LoginTest_LoginSuccesfullyManagerRole()
        {
            var controler = new LoginController();
            controler.ControllerContext = new ControllerContext(setContext(), controler);
            var model = new LoginModel();
            model.accountName = "SonNT";
            model.password = "Son123";
            var result = controler.Index(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("HangHoa/Home", result.RouteValues["controller"]);
        }

        //test login multiple role
        [TestMethod()]
        public void LoginTest_LoginSuccesfullyMultipleRole()
        {
            var controler = new LoginController();
            controler.ControllerContext = new ControllerContext(setContext(), controler);
            var model = new LoginModel();
            model.accountName = "BanTQ";
            model.password = "Ban123";
            var result = controler.Index(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
            Assert.AreEqual("PhanPhoi", result.RouteValues["area"]);
        }

        //test save cookie
        [TestMethod()]
        public void LoginTest_SaveCookie()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();

            response.Setup(r => r.Cookies).Returns(new HttpCookieCollection());
            request.SetupGet(r => r.Cookies).Returns(new HttpCookieCollection());
            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            

            RequestContext rc = new RequestContext(context.Object, new RouteData());
            var controler = new LoginController();
            controler.ControllerContext = new ControllerContext(rc, controler);

            var model = new LoginModel();
            model.accountName = "DatLM";
            model.password = "Dat123";
            model.rememberMe = true;
            var result = controler.Index(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("PhanPhoi/Home", result.RouteValues["controller"]);
        }

        //Test cookie sales
        [TestMethod()]
        public void LoginTest_CookieSalesRole()
        {
            var controler = new LoginController();
            controler.ControllerContext = new ControllerContext(setContext(), controler);
            var model = new LoginModel();
            model.accountName = "DatLM";
            model.password = "Dat123";
            var result = controler.Index(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("PhanPhoi/Home", result.RouteValues["controller"]);
        }

        //test cookie accountant
        [TestMethod()]
        public void LoginTest_CookieSuccesfullyAccountantRole()
        {
            var controler = new LoginController();
            controler.ControllerContext = new ControllerContext(setContext(), controler);
            var model = new LoginModel();
            model.accountName = "MinhTA";
            model.password = "Minh123";
            var result = controler.Index(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("KeToan/Home", result.RouteValues["controller"]);
        }

        //test cookie warehouse
        [TestMethod()]
        public void LoginTest_CookieSuccesfullyWarehouseRole()
        {
            var context = new Mock<ControllerContext>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(m => m.HttpContext.Session).Returns(session.Object);
            var controler = new LoginController();
            controler.ControllerContext = context.Object;
            var model = new LoginModel();
            model.accountName = "SonNT";
            model.password = "Son123";
            var result = controler.Index(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("HangHoa/Home", result.RouteValues["controller"]);
        }

        //test cookie admin
        [TestMethod()]
        public void LoginTest_CookieAdministratorRole()
        {
            var controler = new LoginController();
            controler.ControllerContext = new ControllerContext(setContext(), controler);
            var model = new LoginModel();
            model.accountName = "SonNT";
            model.password = "Son123";
            var result = controler.Index(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("HangHoa/Home", result.RouteValues["controller"]);
        }

        //test cookie manager
        [TestMethod()]
        public void LoginTest_CookieManagerRole()
        {
            var controler = new LoginController();
            controler.ControllerContext = new ControllerContext(setContext(), controler);
            var model = new LoginModel();
            model.accountName = "SonNT";
            model.password = "Son123";
            var result = controler.Index(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("HangHoa/Home", result.RouteValues["controller"]);
        }

        /*
         * Test for forgot password view
         **/

        //test redirect password
        [TestMethod]
        public void TestForgotPassword_ForRedirect()
        {
            var controller = new LoginController();
            var result = controller.ForgotPassword() as RedirectToRouteResult;
            Assert.AreEqual("ForgotPassword", result.RouteValues["action"]);
            Assert.AreEqual("Login", result.RouteValues["controller"]);
        }

        //test account not exist
        [TestMethod]
        public void TestForgotPassword_AccountNotExits()
        {
            var controller = new LoginController();
            var model = new LoginModel();
            model.accountName = "Cuong123456";
            var result = controller.ForgotPassword(model) as ViewResult;
            var returnMsg = result.ViewData.ModelState.Values.First().Errors.First().ErrorMessage;
            Assert.AreEqual("Tài khoản không tồn tại", returnMsg);
        }

        //test succesfully
        [TestMethod]
        public void TestForgotPassword_SendSuccesfully()
        {
            var controller = new LoginController();
            var model = new LoginModel();
            model.accountName = "BanTQ";
            var result = controller.ForgotPassword(model) as ViewResult;
            var returnMsg = result.ViewData.ModelState.Values.First().Errors.First().ErrorMessage;
            Assert.AreEqual("Yêu cầu của bạn đã được gửi đi", returnMsg);
        }
    }
}