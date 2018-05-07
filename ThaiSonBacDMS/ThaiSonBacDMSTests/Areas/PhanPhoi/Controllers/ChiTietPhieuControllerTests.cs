using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThaiSonBacDMS.Areas.PhanPhoi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;
using ThaiSonBacDMS.Common;
using Moq;
using ThaiSonBacDMS.Models;
using ThaiSonBacDMS.Controllers.Tests;
using ThaiSonBacDMS.Controllers;
using System.Web.Routing;
using System.Web;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers.Tests
{
    [TestClass()]
    public class ChiTietPhieuControllerTests
    {

        [TestMethod]
        public void TestIndexForRedirect()
        {
            var controller = new ChiTietPhieuController();
            var result = controller.Index("") as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void TestIndexForViewResult()
        {
            var controller = new ChiTietPhieuController();
            var result = controller.Index("O1");
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void TestIndexForViewData()
        {
            var controller = new ChiTietPhieuController();
            var result = controller.Index("O1") as ViewResult;
            if (result != null)
            {
                Assert.IsInstanceOfType(result.Model, typeof(OrderTotalModel));
                var model = result.Model as OrderTotalModel;
                if (model != null)
                {
                    Assert.IsTrue(model.readItems.Count == 8);
                }
            }
        }

        [TestMethod]
        public void TestOnetimeDeliveryForRedirect()
        {
            var controller = new ChiTietPhieuController();
            var result = controller.OnetimeDelivery("") as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void TestOnetimeDeliveryForViewResult()
        {
            var controller = new ChiTietPhieuController();
            var result = controller.OnetimeDelivery("O32");
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void TestOnetimeDeliveryForViewData()
        {
            var controller = new ChiTietPhieuController();
            var result = controller.OnetimeDelivery("O32") as ViewResult;
            if (result != null)
            {
                Assert.IsInstanceOfType(result.Model, typeof(OrderTotalModel));
                var model = result.Model as OrderTotalModel;
                if (model != null)
                {
                    Assert.IsTrue(model.readItems.Count == 3);
                }
            }
        }

        [TestMethod]
        public void TestDetailStatusForRedirect()
        {
            var controller = new ChiTietPhieuController();
            var result = controller.DetailStatus("") as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void TestDetailStatusForViewResult()
        {
            var controller = new ChiTietPhieuController();
            var result = controller.DetailStatus("O32");
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void TestDetailStatusForViewData()
        {
            var controller = new ChiTietPhieuController();
            var result = controller.DetailStatus("O32") as ViewResult;
            if (result != null)
            {
                Assert.IsInstanceOfType(result.Model, typeof(OrderTotalModel));
                var model = result.Model as OrderTotalModel;
                if (model != null)
                {
                    Assert.IsTrue(model.customerName.Equals("Công ty Thanh Vinh"));
                }
            }
        }

        [TestMethod]
        public void TestMultipleDeliveryForRedirect()
        {
            var controller = new ChiTietPhieuController();
            var result = controller.MultipleDelivery("") as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void TestMultipleDeliveryForViewResult()
        {
            var controller = new ChiTietPhieuController();
            var result = controller.MultipleDelivery("O31");
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void TestMultipleDeliveryForViewData()
        {
            var controller = new ChiTietPhieuController();
            var result = controller.MultipleDelivery("O31") as ViewResult;
            if (result != null)
            {
                Assert.IsInstanceOfType(result.Model, typeof(OrderTotalModel));
                var model = result.Model as OrderTotalModel;
                if (model != null)
                {
                    Assert.IsTrue(model.readPart.Count == 2);
                }
            }
        }

        [TestMethod]
        public void TestCheckOutFail()
        {
            var controller = new ChiTietPhieuController();
            var result = controller.CheckOut("") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["error"]);
        }

        [TestMethod]
        public void TestCheckOutSuccess()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { user_id = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new ChiTietPhieuController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.CheckOut("O1") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }

        [TestMethod]
        public void TestCancelOrderFail()
        {
            var controller = new ChiTietPhieuController();
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
            var controller = new ChiTietPhieuController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.CancelOrder("O1", "Reason") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
    }
}