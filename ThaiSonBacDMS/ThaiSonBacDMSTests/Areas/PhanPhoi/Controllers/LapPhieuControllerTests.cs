using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThaiSonBacDMS.Areas.PhanPhoi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;
using System.Web.Mvc;
using ThaiSonBacDMS.Common;
using Moq;
using Models.DAO_Model;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers.Tests
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
            var controller = new LapPhieuController();
            var result = controller.ChangeCustomer(0) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("LapPhieu", result.RouteValues["controller"]);
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
                Assert.AreEqual(14, data.Count);
            }
        }

        [TestMethod]
        public void TestSuaPhieuForRedirect()
        {
            var controller = new LapPhieuController();
            var result = controller.SuaPhieu("") as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void TestSuaPhieuForViewResult()
        {
            var controller = new LapPhieuController();
            var result = controller.SuaPhieu("O1");
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void TestSuaPhieuForViewData()
        {
            var controller = new LapPhieuController();
            var result = controller.SuaPhieu("O1") as ViewResult;
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
        public void TestCheckQttForRedirect()
        {
            var controller = new LapPhieuController();
            var result = controller.CheckQtt(0) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("LapPhieu", result.RouteValues["controller"]);
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
            var lst = new List<CustomOrderItem>();
            var controller = new LapPhieuController();
            var result = controller.Steal(lst) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("LapPhieu", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void TestStealForViewResult()
        {
            var lst = new List<CustomOrderItem>();
            var controller = new LapPhieuController();
            var result = controller.Steal(lst);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
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
        public void TestSaveOrderForRedirect()
        {
            var controller = new LapPhieuController();
            var result = controller.SaveOrder(new OrderTotalModel { }) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("LapPhieu", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void TestSaveOrderForViewResult()
        {
            var controller = new LapPhieuController();
            var result = controller.SaveOrder(new OrderTotalModel
            {

            });
            Assert.IsInstanceOfType(result, typeof(ViewResult));
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
            Assert.AreEqual("LapPhieu", result.RouteValues["controller"]);
        }

        [TestMethod]
        public void TestCheckOutForViewResult()
        {
            var controller = new LapPhieuController();
            var result = controller.CheckOut(new OrderTotalModel
            {

            });
            Assert.IsInstanceOfType(result, typeof(ViewResult));
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
            var fakeHttpContext = new Mock<UserSession>();
            fakeHttpContext.Setup(p => p.user_id).Returns(3);
            var controller = new LapPhieuController();
            var result = controller.CancelOrder("O1", "Reason") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
    }
}