using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThaiSonBacDMS.Areas.PhanPhoi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;
using System.Web.Mvc;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers.Tests
{
    [TestClass()]
    public class GiaSanPhamControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var controller = new GiaSanPhamController();
            var result = controller.Index() as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.map.Count > 0);
        }
        //search by all value is null
        [TestMethod()]
        public void SearchBy_AllNullValue()
        {
            var controller = new GiaSanPhamController();
            var model = new ProductPhanPhoiModel();
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.map.Count > 0);
        }
        //search by product name
        [TestMethod()]
        public void SearchBy_ProductName()
        {
            var controller = new GiaSanPhamController();
            var model = new ProductPhanPhoiModel();
            model.pCodeSearch = "-200";
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.map.Count > 0);
        }
        //search by category 
        [TestMethod]
        public void SearchBy_Category()
        {
            var controller = new GiaSanPhamController();
            var model = new ProductPhanPhoiModel();
            model.categorySearch = "2";
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.map.Count > 0);
        }
        //search by from price before vat
        [TestMethod()]
        public void SearchBy_Price_From_BeforeVAT()
        {
            var controller = new GiaSanPhamController();
            var model = new ProductPhanPhoiModel();
            model.priceFrom = "1,500,000";
            model.VAT = "1";
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.map.Count > 0);
        }
        //search by from price after vat
        [TestMethod()]
        public void SearchBy_Price_From_AfterVAT()
        {
            var controller = new GiaSanPhamController();
            var model = new ProductPhanPhoiModel();
            model.priceFrom = "150,000";
            model.VAT = "0";
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.map.Count > 0);
        }

        //search by price to before VAT
        [TestMethod()]
        public void SearchBy_PriceTo_Before_VAT()
        {
            var controller = new GiaSanPhamController();
            var model = new ProductPhanPhoiModel();
            model.priceTo = "150,000";
            model.VAT = "1";
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.map.Count > 0);
        }
        //search by price to after VAT
        [TestMethod()]
        public void SearchBy_PriceTo_After_VAT()
        {
            var controller = new GiaSanPhamController();
            var model = new ProductPhanPhoiModel();
            model.priceTo = "3,500,000";
            model.VAT = "0";
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.map.Count > 0);
        }
        //search by inrange [priceFrom,priceTo] before vat
        [TestMethod()]
        public void SearchBy_InRange_Before_VAT()
        {
            var controller = new GiaSanPhamController();
            var model = new ProductPhanPhoiModel();
            model.VAT = "1";
            model.priceFrom = "1,500,000";
            model.priceTo = "200,000,000";
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.map.Count > 0);
        }
        //search by inrange [priceFrom,priceTo] after vat
        [TestMethod()]
        public void SearchBy_InRange_After_VAT()
        {
            var controller = new GiaSanPhamController();
            var model = new ProductPhanPhoiModel();
            model.VAT = "0";
            model.priceFrom = "1,500,000";
            model.priceTo = "200,000,000";
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.map.Count > 0);
        }
    }
}