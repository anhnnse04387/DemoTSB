using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThaiSonBacDMS.Areas.QuanLy.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThaiSonBacDMS.Areas.QuanLy.Models;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers.Tests
{
    [TestClass()]
    public class BaoCaoChiTietDoanhThuControllerTest
    {
        [TestMethod()]
        public void IndexTest_ListShowYear()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            var result = controler.Index() as ViewResult;
            var expectedValue = "2018";
            var model = result.Model as BaoCaoCongNoKhachHangModel;
            Assert.AreEqual(model.listShowYear.First().Value, expectedValue);
        }

        [TestMethod()]
        public void IndexTest_ListCategory()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            var result = controler.Index() as ViewResult;
            var expectedValue = 5;
            var model = result.Model as BaoCaoCongNoKhachHangModel;
            Assert.AreEqual(model.listCategory.Count, expectedValue);

        }

        [TestMethod()]
        public void IndexTest_ListCongNo()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            var result = controler.Index() as ViewResult;
            var expectedValue = 9;
            var model = result.Model as BaoCaoCongNoKhachHangModel;
            Assert.AreEqual(model.listCongNo.Count, expectedValue);

        }

        [TestMethod()]
        public void IndexTest_TotalPrice()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            var result = controler.Index() as ViewResult;
            var expectedValue = 0;
            var model = result.Model as BaoCaoCongNoKhachHangModel;
            Assert.AreEqual(model.totalPrice, expectedValue);

        }

        [TestMethod()]
        public void ChangeDataTest_SelectedDayNull()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            BaoCaoCongNoKhachHangModel model = new BaoCaoCongNoKhachHangModel();
            model.selectedDay = null;
            var result = controler.ChangeData(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void ChangeDataTest_SelectedMonthNull()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            BaoCaoCongNoKhachHangModel model = new BaoCaoCongNoKhachHangModel();
            model.selectedMonth = null;
            var result = controler.ChangeData(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void ChangeDataTest_SelectedYearNull()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            BaoCaoCongNoKhachHangModel model = new BaoCaoCongNoKhachHangModel();
            model.selectedMonth = null;
            var result = controler.ChangeData(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void ChangeDataTest_SelectedDayEmpty()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            BaoCaoCongNoKhachHangModel model = new BaoCaoCongNoKhachHangModel();
            model.selectedDay = string.Empty;
            var result = controler.ChangeData(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void ChangeDataTest_SelectedMonthEmpty()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            BaoCaoCongNoKhachHangModel model = new BaoCaoCongNoKhachHangModel();
            model.selectedMonth = string.Empty;
            var result = controler.ChangeData(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void ChangeDataTest_SelectedYearEmpty()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            BaoCaoCongNoKhachHangModel model = new BaoCaoCongNoKhachHangModel();
            model.selectedMonth = string.Empty;
            var result = controler.ChangeData(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void ChangeDataTest_SelectedDayWrongFormat()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            BaoCaoCongNoKhachHangModel model = new BaoCaoCongNoKhachHangModel();
            model.selectedDay = "abc";
            var result = controler.ChangeData(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void ChangeDataTest_SelectedMonthWrongFormat()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            BaoCaoCongNoKhachHangModel model = new BaoCaoCongNoKhachHangModel();
            model.selectedMonth = "abc";
            var result = controler.ChangeData(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void ChangeDataTest_SelectedYearWrongFormat()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            BaoCaoCongNoKhachHangModel model = new BaoCaoCongNoKhachHangModel();
            model.selectedYear = "abc";
            var result = controler.ChangeData(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void ChangeDataTest_SelectedDayTrueFormat()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            BaoCaoCongNoKhachHangModel model = new BaoCaoCongNoKhachHangModel();
            model.selectedDay = "23";
            var result = controler.ChangeData(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void ChangeDataTest_SelectedMonthTrueFormat()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            BaoCaoCongNoKhachHangModel model = new BaoCaoCongNoKhachHangModel();
            model.selectedMonth = "1";
            var result = controler.ChangeData(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod()]
        public void ChangeDataTest_SelectedYearTrueFormat()
        {
            var controler = new BaoCaoCongNoKhachHangController();
            BaoCaoCongNoKhachHangModel model = new BaoCaoCongNoKhachHangModel();
            model.selectedMonth = "";
            var result = controler.ChangeData(model) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}