using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThaiSonBacDMS.Areas.QuanLy.Controllers;
using ThaiSonBacDMS.Areas.QuanLy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ThaiSonBacDMS.Common;
using Moq;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers.Tests
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
            var fakeHttpContext = new Mock<UserSession>();
            fakeHttpContext.Setup(p => p.user_id).Returns(3);
            var controller = new ChiTietPhieuController();
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
            var fakeHttpContext = new Mock<UserSession>();
            fakeHttpContext.Setup(p => p.user_id).Returns(3);
            var controller = new ChiTietPhieuController();
            var result = controller.CancelOrder("O1", "Reason") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
    }
}