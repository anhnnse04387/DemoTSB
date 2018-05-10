using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThaiSonBacDMS.Areas.QuanLy.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThaiSonBacDMS.Areas.QuanLy.Models;
using System.Web.Mvc;
using Moq;
using ThaiSonBacDMS.Common;
using Models.DAO_Model;
using System.Web;
using System.Web.Routing;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers.Tests
{
    [TestClass()]
    public class DanhSachCongNoCungCapControllerTest
    {
        [TestMethod()]
        public void IndexTest()
        {
            var controller = new DanhSachCongNoCungCapController();
            var result = controller.Index() as ViewResult;
            var x = result.Model as DanhSachCongNoCungCapModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search by all values is null
        [TestMethod()]
        public void TestSearchBy_All_NullValues()
        {
            var controller = new DanhSachCongNoCungCapController();
            var result = controller.Index("", "", "") as ViewResult;
            var x = result.Model as DanhSachCongNoCungCapModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //test insert with all values are null
        [TestMethod()]
        public void InsertData_Without_All_Null_Value()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { accountID = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new DanhSachCongNoCungCapController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.InsertData("", "", "", "", "", "") as JsonResult;
            IDictionary<string, object> data = (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(false, data["success"]);
        }
        //test insert with supplier id is character
        [TestMethod()]
        public void InserData_With_Supplier_Id_Is_Character()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { accountID = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new DanhSachCongNoCungCapController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.InsertData("a", "2,000,000", "500,000", "1,500,000", "0", "") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
        //test insert with supplier id is null
        [TestMethod()]
        public void InsertData_Without_Supplier_Id()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { accountID = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new DanhSachCongNoCungCapController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.InsertData("", "2,000,000", "500,000", "1,500,000", "0", "") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
        //test insert with thanhToan is null
        [TestMethod()]
        public void InsertData_With_ThanhToan_Is_Null()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { accountID = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new DanhSachCongNoCungCapController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.InsertData("1", "2,000,000", "", "1,500,000", "0", "") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
        //test insert with thanhToan is character
        [TestMethod()]
        public void InsertData_With_ThanhToan_Is_Character()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { accountID = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new DanhSachCongNoCungCapController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.InsertData("1", "2,000,000", "a", "1,500,000", "0", "") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
        //test insert with nhapTrongKy is null
        [TestMethod()]
        public void InsertData_With_NhapTrongKy_Is_Null()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { accountID = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new DanhSachCongNoCungCapController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.InsertData("1", "", "2,000,000", "1,500,000", "0", "") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
        //test insert with nhapTrongKy is character
        [TestMethod()]
        public void InsertData_With_NhapTrongKy_Is_Character()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { accountID = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new DanhSachCongNoCungCapController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.InsertData("1", "a", "2,000,000", "1,500,000", "0", "") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
        //test insert with valid values need session
        [TestMethod()]
        public void InsertData_With_Validate_Values()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { accountID = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new DanhSachCongNoCungCapController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.InsertData("1", "4000000", "2000000", "2000000", "5000000", "tra tien hang") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
        //test autocomplete function with null value
        [TestMethod()]
        public void autoCompleteTest()
        {
            var controller = new DanhSachCongNoCungCapController();
            var result = controller.autoComplete("") as JsonResult;
            var x = result.Data as List<Autocomplete>;
            Assert.AreEqual(0, x.Count);
        }
        //test autocomplete function with value
        [TestMethod()]
        public void Test_Autocomplete_Function_With_Value()
        {
            var controller = new DanhSachCongNoCungCapController();
            var result = controller.autoComplete("LS") as JsonResult;
            var x = result.Data as List<Autocomplete>;
            Assert.IsTrue(x.Count > 0);
        }

        //test search with supplier name
        [TestMethod()]
        public void TestSearch_By_Supplier_Name()
        {
            var controller = new DanhSachCongNoCungCapController();
            var model = new DanhSachCongNoCungCapModel();
            var result = controller.Index("Thái Nam Sơn", "", "") as ViewResult;
            var x = result.Model as DanhSachCongNoCungCapModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //test seach with con no tu
        [TestMethod()]
        public void TestSearch_By_Debt_From()
        {
            var controller = new DanhSachCongNoCungCapController();
            var model = new DanhSachCongNoCungCapModel();
            var result = controller.Index("", "100,000,000", "") as ViewResult;
            var x = result.Model as DanhSachCongNoCungCapModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //test search with con no den
        [TestMethod()]
        public void TestSearch_By_Debt_To()
        {
            var controller = new DanhSachCongNoCungCapController();
            var model = new DanhSachCongNoCungCapModel();
            var result = controller.Index("", "", "200,000,000") as ViewResult;
            var x = result.Model as DanhSachCongNoCungCapModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }

        //test search by debt from is character
        public void TestSearch_By_Debt_From_Is_Character()
        {
            var controller = new DanhSachCongNoCungCapController();
            var model = new DanhSachCongNoCungCapModel();
            var result = controller.Index("", "s", "") as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}