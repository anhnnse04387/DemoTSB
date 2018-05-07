using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThaiSonBacDMS.Areas.PhanPhoi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.PhanPhoi.Models;

namespace ThaiSonBacDMS.Areas.PhanPhoi.Controllers.Tests
{
    [TestClass()]
    public class SanPhamDangKinhDoanhControllerTests
    {
        [TestMethod()]
        public void Index()
        {
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.Index() as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }

        //test all null value search
        [TestMethod()]
        public void TestSearchDataIsNull()
        {
            var model = new ProductPhanPhoiModel();
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);

        }
        [TestMethod()]
        public void TestSearch_BySupplier()
        {
            var model = new ProductPhanPhoiModel();
            model.supplierSearch = "2";
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        [TestMethod()]
        public void TestSearch_ByProductName()
        {
            var model = new ProductPhanPhoiModel();
            model.categorySearch = "10";
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        [TestMethod()]
        public void TestSearch_ByNameAndSupplier()
        {
            var model = new ProductPhanPhoiModel();
            model.categorySearch = "1";
            model.supplierSearch = "2";
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search date from
        [TestMethod()]
        public void TestSearch_ByDateImport_From()
        {
            var model = new ProductPhanPhoiModel();
            model.fromDate = "05/22/2018";
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search date to
        [TestMethod()]
        public void TestSearch_ByDateImport_To()
        {
            var model = new ProductPhanPhoiModel();
            model.toDate = "05/22/2018";
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search date from and date to
        [TestMethod()]
        public void TestSearch_ByDateImport_From_And_To()
        {
            var model = new ProductPhanPhoiModel();
            model.fromDate = "04/22/2018";
            model.toDate = "05/22/2018";
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search by category
        [TestMethod()]
        public void TestSearch_ByCategory()
        {
            var model = new ProductPhanPhoiModel();
            model.categorySearch = "2";
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search by category and supplier 
        [TestMethod()]
        public void TestSearch_ByCategory_And_Supplier()
        {
            var model = new ProductPhanPhoiModel();
            model.supplierSearch = "1";
            model.categorySearch = "2";
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search by supplier and product name
        [TestMethod()]
        public void TestSearch_BySupplier_And_ProductName()
        {
            var model = new ProductPhanPhoiModel();
            model.supplierSearch = "1";
            model.productIdSearch = "4";
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search by supplier and date import from
        [TestMethod()]
        public void TestSearch_BySupplier_And_DateImportFrom()
        {
            var model = new ProductPhanPhoiModel();
            model.supplierSearch = "1";
            model.fromDate = "04/15/2018";
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search by supplier and date import to
        [TestMethod()]
        public void TestSearch_BySupplier_And_DateImportTo()
        {
            var model = new ProductPhanPhoiModel();
            model.supplierSearch = "1";
            model.toDate = "05/12/2018";
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search by all value
        [TestMethod()]
        public void TestSearch_ByAllValue()
        {
            var model = new ProductPhanPhoiModel();
            model.supplierSearch = "1";
            model.productIdSearch = "4";
            model.fromDate = "04/12/2017";
            model.toDate = "05/25/2018";
            model.categorySearch = "2";
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }

        //test function search by value is null
        [TestMethod()]
        public void TestSearchValueIsNull()
        {
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.GetSearchValue("") as JsonResult;
            Assert.IsTrue(result.Data != null);
        }
        //test function search by value is not null
        [TestMethod()]
        public void TestSearchValueIsNotNull()
        {
            var controller = new SanPhamDangKinhDoanhController();
            var result = controller.GetSearchValue("MC150") as JsonResult;
            Assert.IsTrue(result.Data != null);
        }
    }
}