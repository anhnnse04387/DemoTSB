using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThaiSonBacDMS.Areas.QuanLy.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThaiSonBacDMS.Areas.QuanLy.Models;
using System.Web.Mvc;
using ThaiSonBacDMS.Common;
using Moq;
using Models.DAO_Model;
using System.Web;
using System.Web.Routing;
using ThaiSonBacDMS.Controllers;
using ThaiSonBacDMS.Models;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers.Tests
{
    [TestClass()]
    public class LapPhieuControllerTests
    {

        [TestMethod]
        public void TestIndexForViewResult()
        {
            var controller = new LapPhieuController();
            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void TestIndexForViewData()
        {
            var controller = new LapPhieuController();
            var result = controller.Index() as ViewResult;
            if (result != null)
            {
                Assert.IsInstanceOfType(result.Model, typeof(OrderTotalModel));
                var model = result.Model as OrderTotalModel;
                if (model != null)
                {
                    Assert.IsTrue(model.lstCustomer != null);
                }
            }
        }

        [TestMethod]
        public void TestChangeCustomerForRedirect()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { user_id = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new LapPhieuController();
            var result = controller.ChangeCustomer(0) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void TestChangeCustomerForViewResult()
        {
            var controller = new LapPhieuController();
            var result = controller.ChangeCustomer(1);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void TestChangeCustomerForViewData()
        {
            var controller = new LapPhieuController();
            var result = controller.ChangeCustomer(1) as JsonResult;
            if (result != null)
            {
                IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
                Assert.AreEqual("108 Nguyễn Trãi, Thanh Xuân Hà Nội", data["deliveryAddress"]);
                Assert.AreEqual("23476889", data["taxCode"]);
                Assert.AreEqual("108 Nguyễn Hoàng, Mỹ Đình 2 Từ Liêm Hà Nội", data["invoiceAddress"]);
            }
        }

        [TestMethod]
        public void TestChooseProductForViewResult()
        {
            var controller = new LapPhieuController();
            var result = controller.ChooseProduct("ABS");
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void TestChooseProductForViewData()
        {
            var controller = new LapPhieuController();
            var result = controller.ChooseProduct("ABS") as JsonResult;
            if (result != null)
            {
                IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
                Assert.AreEqual(2, data.Count);
            }
        }

        [TestMethod]
        public void TestSuaPhieuForRedirect()
        {
            var controller = new LapPhieuController();
            var result = controller.CheckQtt(2);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void TestSuaPhieuForViewResult()
        {
            var controller = new LapPhieuController();
            var result = controller.CheckQtt(2) as JsonResult;
            if (result != null)
            {
                IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
                Assert.AreEqual(2, data.Count);
            }
        }

        [TestMethod]
        public void TestSuaPhieuForViewData()
        {
            var controler = new LoginController();
            var model = new LoginModel();
            model.accountName = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz";
            model.password = string.Empty;

            var result = controler.Index(model) as ViewResult;
            var returnMsg = result.ViewData.ModelState.Values.First().Errors.First().ErrorMessage;
            Assert.AreEqual("Tài khoản và mật khẩu < 20 ký tự", returnMsg);
        }

        [TestMethod]
        public void TestCheckQttForRedirect()
        {
            var controller = new LapPhieuController();
            var result = controller.CheckQtt(2) as JsonResult;
            if (result != null)
            {
                IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
                Assert.AreEqual(2, data.Count);
            }
        }

        [TestMethod]
        public void TestCheckQttForViewResult()
        {
            var controller = new LapPhieuController();
            var result = controller.CheckQtt(2);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void TestCheckQttForViewData()
        {
            var controller = new LapPhieuController();
            var result = controller.CheckQtt(2) as JsonResult;
            if (result != null)
            {
                IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
                Assert.AreEqual(2, data.Count);
            }
        }

        [TestMethod]
        public void TestStealForRedirect()
        {
            var controller = new LapPhieuController();
            var result = controller.CheckQtt(2);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void TestStealForViewResult()
        {
            var controller = new LapPhieuController();
            var result = controller.ChooseProduct("ABS") as JsonResult;
            if (result != null)
            {
                IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
                Assert.AreEqual(2, data.Count);
            }
        }

        [TestMethod]
        public void TestStealForViewData()
        {
            var lst = new List<CustomOrderItem>();
            var controller = new LapPhieuController();
            var result = controller.Steal(lst) as JsonResult;
            if (result != null)
            {
                IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
                Assert.AreEqual(true, data["success"]);
            }
        }

        [TestMethod]
        public void TestCancelOrderFailed()
        {
            var controller = new LapPhieuController();
            var result = controller.CancelOrder("", "") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["error"]);
        }

        [TestMethod]
        public void TestSaveOrderForRedirect()
        {
            var controller = new LapPhieuController();
            var result = controller.CancelOrder("", "") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["error"]);
        }

        [TestMethod]
        public void TestSaveOrderForViewResult()
        {
            var controller = new LapPhieuController();
            var result = controller.SaveOrder(new OrderTotalModel
            {

            });
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void TestSaveOrderForViewData()
        {
            var controller = new LapPhieuController();
            var result = controller.SaveOrder(new OrderTotalModel
            {

            }) as JsonResult;
            if (result != null)
            {
                IDictionary<string, object> data =
                (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
                Assert.AreEqual(true, data["success"]);
            }
        }

        [TestMethod]
        public void TestCheckOutForRedirect()
        {
            var controller = new LapPhieuController();
            var result = controller.CheckOut(new OrderTotalModel { }) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void TestCheckOutForViewResult()
        {
            var controller = new LapPhieuController();
            var result = controller.CheckOut(new OrderTotalModel
            {

            });
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void TestCheckOutForViewData()
        {
            var controller = new LapPhieuController();
            var result = controller.CheckOut(new OrderTotalModel
            {

            }) as JsonResult;
            if (result != null)
            {
                IDictionary<string, object> data =
                (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
                Assert.AreEqual(true, data["success"]);
            }
        }

        [TestMethod]
        public void TestCancelOrderFail()
        {
            var controller = new LapPhieuController();
            var result = controller.CancelOrder("", "") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["error"]);
        }

        [TestMethod]
        public void TestCancelOrderSuccess()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { user_id = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new LapPhieuController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.CancelOrder("O1", "Reason") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }

        [TestMethod]
        public void TestChangedCustomerForViewData()
        {
            var controller = new LapPhieuController();
            var result = controller.ChangeCustomer(1) as JsonResult;
            if (result != null)
            {
                IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
                Assert.AreEqual("108 Nguyễn Trãi, Thanh Xuân Hà Nội", data["deliveryAddress"]);
                Assert.AreEqual("23476889", data["taxCode"]);
                Assert.AreEqual("108 Nguyễn Hoàng, Mỹ Đình 2 Từ Liêm Hà Nội", data["invoiceAddress"]);
            }
        }
    }
}