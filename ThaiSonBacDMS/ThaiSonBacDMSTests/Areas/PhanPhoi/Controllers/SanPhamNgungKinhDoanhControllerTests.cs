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
    public class SanPhamNgungKinhDoanhControllerTests
    {
        [TestMethod()]
        public void GetSearchValueTest()
        {
            var controller = new SanPhamNgungKinhDoanhController();
            var result = controller.Index() as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }

        //search by all value is null
        [TestMethod()]
        public void TestSearchBy_NullValues()
        {
            var controller = new SanPhamNgungKinhDoanhController();
            var model = new ProductPhanPhoiModel();
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search by product name
        [TestMethod]
        public void TestSearchBy_ProductName()
        {
            var controller = new SanPhamNgungKinhDoanhController();
            var model = new ProductPhanPhoiModel();
            model.productIdSearch = "3";
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);

        }
        //search by supplier
        [TestMethod()]
        public void TestSearchBy_Supplier()
        {
            var controller = new SanPhamNgungKinhDoanhController();
            var model = new ProductPhanPhoiModel();
            model.supplierSearch = "9";
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search by date import from
        [TestMethod()]
        public void TestSearchBy_DateImport_From()
        {
            var controller = new SanPhamNgungKinhDoanhController();
            var model = new ProductPhanPhoiModel();
            model.fromDate = "03/10/2018";
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search by date import to
        [TestMethod()]
        public void TestSearchBy_DateImport_To()
        {
            var controller = new SanPhamNgungKinhDoanhController();
            var model = new ProductPhanPhoiModel();
            model.toDate = "03/10/2018";
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search by date import from and to
        [TestMethod()]
        public void TestSearchBy_DateImport_From_To()
        {
            var controller = new SanPhamNgungKinhDoanhController();
            var model = new ProductPhanPhoiModel();
            model.fromDate = "03/10/2018";
            model.toDate = "05/25/2018";
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search by category
        [TestMethod()]
        public void TestSearchBy_Category()
        {
            var controller = new SanPhamNgungKinhDoanhController();
            var model = new ProductPhanPhoiModel();
            model.categorySearch = "10";
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //search all values
        [TestMethod()]
        public void TestSearchBy_AllValue()
        {
            var controller = new SanPhamNgungKinhDoanhController();
            var model = new ProductPhanPhoiModel();
            model.productIdSearch = "2";
            model.categorySearch = "1";
            model.supplierSearch = "1";
            model.fromDate = "03/10/2018";
            model.toDate = "05/25/2018";
            var result = controller.Index(model) as ViewResult;
            var x = result.Model as ProductPhanPhoiModel;
            Assert.IsTrue(x.lstDisplay.Count > 0);
        }
        //test search value is null
        [TestMethod()]
        public void TestSearchNameIsNull()
        {
            var controller = new SanPhamNgungKinhDoanhController();
            var result = controller.GetSearchValue("") as JsonResult;
            var x = result.Data;
            Assert.IsTrue(x !=null);
            
        }
        //test search value is not null
        [TestMethod()]
        public void TestSearchNameIsNotNull()
        {
            var controller = new SanPhamNgungKinhDoanhController();
            var result = controller.GetSearchValue("MC2") as JsonResult;
            var x = result.Data;
            Assert.IsTrue(x != null);

        }
    }
}