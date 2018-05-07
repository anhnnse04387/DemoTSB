using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.DAO_Model;
using System.Collections.Generic;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers.Tests
{
    [TestClass()]
    public class DanhSachCongNoCungCapControllerTests
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
            var controller = new DanhSachCongNoCungCapController();
            var result = controller.InsertData("", "", "", "", "", "") as JsonResult;
            IDictionary<string, object> data = (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(false, data["success"]);
        }
        //test insert with supplier id is character
        [TestMethod()]
        public void InserData_With_Supplier_Id_Is_Character()
        {
            var controller = new DanhSachCongNoCungCapController();
            var result = controller.InsertData("a", "2,000,000", "500,000", "1,500,000", "0", "") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
        //test insert with supplier id is null
        [TestMethod()]
        public void InsertData_Without_Supplier_Id()
        {
            var controller = new DanhSachCongNoCungCapController();
            var result = controller.InsertData("", "2,000,000", "500,000", "1,500,000", "0", "") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
        //test insert with thanhToan is null
        [TestMethod()]
        public void InsertData_With_ThanhToan_Is_Null()
        {
            var controller = new DanhSachCongNoCungCapController();
            var result = controller.InsertData("1", "2,000,000", "", "1,500,000", "0", "") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
        //test insert with thanhToan is character
        [TestMethod()]
        public void InsertData_With_ThanhToan_Is_Character()
        {
            var controller = new DanhSachCongNoCungCapController();
            var result = controller.InsertData("1", "2,000,000", "a", "1,500,000", "0", "") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
        //test insert with nhapTrongKy is null
        [TestMethod()]
        public void InsertData_With_NhapTrongKy_Is_Null()
        {
            var controller = new DanhSachCongNoCungCapController();
            var result = controller.InsertData("1", "", "2,000,000", "1,500,000", "0", "") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
        //test insert with nhapTrongKy is character
        [TestMethod()]
        public void InsertData_With_NhapTrongKy_Is_Character()
        {
            var controller = new DanhSachCongNoCungCapController();
            var result = controller.InsertData("1", "a", "2,000,000", "1,500,000", "0", "") as JsonResult;
            IDictionary<string, object> data =
            (IDictionary<string, object>)new System.Web.Routing.RouteValueDictionary(result.Data);
            Assert.AreEqual(true, data["success"]);
        }
        //test insert with valid values need session
        [TestMethod()]
        public void InsertData_With_Validate_Values()
        {
            var controller = new DanhSachCongNoCungCapController();
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
            var x = result.Data;
            Assert.AreEqual(null, x);
        }
        //test autocomplete function with value
        [TestMethod()]
        public void Test_Autocomplete_Function_With_Value()
        {
            var controller = new DanhSachCongNoCungCapController();
            var result = controller.autoComplete("LS") as JsonResult;
            var x = result.Data;
            Assert.AreEqual(null, x);
        }

        //test search with
        [TestMethod()]
        public void IndexTest1()
        {
            Assert.Fail();
        }
    }
}