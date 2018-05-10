using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThaiSonBacDMS.Areas.PhanPhoi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;
using Moq;
using System.Web.Routing;
using System.Web;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers.Tests
{
    [TestClass()]
    public class DanhSachCongNoKhachHangControllerTest
    {
        [TestMethod()]
        public void IndexTest()
        {
            var controller = new DanhSachCongNoKhachHangController();
            var result = controller.Index() as ViewResult;
            var x = result.Model as DanhSachCongNoKhachHangModel;
            Assert.IsTrue(x.lstDisplay.Count() > 0);
        }
        //test all value is null
        [TestMethod()]
        public void TestSearch_By_All_Value_Null()
        {
            var controller = new DanhSachCongNoKhachHangController();
            var result = controller.Index("","","") as ViewResult;
            var x = result.Model as DanhSachCongNoKhachHangModel;
            Assert.IsTrue(x.lstDisplay.Count() > 0);
        }
        //test all value customer id is null
        [TestMethod()]
        public void TestSearch_By_CustomerId_Is_Null()
        {
            var controller = new DanhSachCongNoKhachHangController();
            var result = controller.Index("", "5,000", "6,000") as ViewResult;
            var x = result.Model as DanhSachCongNoKhachHangModel;
            Assert.IsTrue(x.lstDisplay.Count() > 0);
        }
        //test all value customer id is not null
        [TestMethod()]
        public void TestSearch_By_CustomerId_Is_Not_Null()
        {
            var controller = new DanhSachCongNoKhachHangController();
            var result = controller.Index("1", "", "") as ViewResult;
            var x = result.Model as DanhSachCongNoKhachHangModel;
            Assert.IsTrue(x.lstDisplay.Count() > 0);
        }
        //test all by debt from is  null
        [TestMethod()]
        public void TestSearch_By_From_Debt_Is_Null()
        {
            var controller = new DanhSachCongNoKhachHangController();
            var result = controller.Index("1", "500,000", "") as ViewResult;
            var x = result.Model as DanhSachCongNoKhachHangModel;
            Assert.IsTrue(x.lstDisplay.Count() > 0);
        }
        //test search with customerId is is character
        [TestMethod()]
        public void TestSearch_By_From_Debt_Is_Character()
        {
            var controller = new DanhSachCongNoKhachHangController();
            var result = controller.Index("a", "", "") as ViewResult;
            var x = result.Model as DanhSachCongNoKhachHangModel;
            Assert.IsTrue(x.lstDisplay.Count() > 0);
        }
        //test search with valid data
        [TestMethod()]
        public void TestSearch_By_Valid_Data()
        {
            var controller = new DanhSachCongNoKhachHangController();
            var result = controller.Index("3", "500,000", "1000,000,000") as ViewResult;
            var x = result.Model as DanhSachCongNoKhachHangModel;
            Assert.IsTrue(x.lstDisplay.Count() > 0);
        }
        //test insert debt with customer id is 0
        [TestMethod()]
        public void TestSearch_Insert_With_CustomerId_Is_0()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { accountID = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new DanhSachCongNoKhachHangController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.UpdateData(0,"100,000","","","","","","") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
        //test insert debt valid data
        [TestMethod()]
        public void TestSearch_Insert_With_Valid_Data()
        {

            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { accountID = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new DanhSachCongNoKhachHangController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.UpdateData(4, "100,000", "10,000", "4,000", "5,000", "tra no", "7,000", "6,000") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
        //test insert debt customer is character
        [TestMethod()]
        public void TestSearch_Insert_With_Character()
        {

            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { accountID = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new DanhSachCongNoKhachHangController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.UpdateData(Convert.ToInt32('a'), "100,000", "10,000", "4,000", "5,000", "tra no", "7,000", "6,000") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
    }
}