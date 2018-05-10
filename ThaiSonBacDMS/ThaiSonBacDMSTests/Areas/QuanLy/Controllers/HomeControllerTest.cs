using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThaiSonBacDMS.Areas.QuanLy.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ThaiSonBacDMS.Areas.QuanLy.Models;
using System.Web.Routing;
using System.Web;
using Moq;
using ThaiSonBacDMS.Common;

namespace ThaiSonBacDMS.Areas.QuanLy.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTest
    {
        public RequestContext setContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();

            response.Setup(r => r.Cookies).Returns(new HttpCookieCollection());
            request.SetupGet(r => r.Cookies).Returns(new HttpCookieCollection());
            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);

            RequestContext rc = new RequestContext(context.Object, new RouteData());
            return rc;
        }

        [TestMethod]
        public void TestIndexForViewResult()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { user_id = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void TestIndexReturnResultValueInMonth()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { user_id = 3 };
            decimal prepareData = 1381800588.0000M;
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.Index() as ViewResult;
            var returnModel = result.Model as HomeModel;
            var x = returnModel.valueInMonth; 
            Assert.AreEqual(x, prepareData);
        }

        [TestMethod]
        public void TestIndexReturnResultOrderInMonth()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { user_id = 3 };
            int prepareData = 17;
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.Index() as ViewResult;
            var returnModel = result.Model as HomeModel;
            var x = returnModel.orderInMonth;
            Assert.AreEqual(x, prepareData);
        }

        [TestMethod]
        public void TestIndexReturnResultNumberCustomer()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { user_id = 3 };
            int prepareData = 3;
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.Index() as ViewResult;
            var returnModel = result.Model as HomeModel;
            var x = returnModel.numberCustomer;
            Assert.AreEqual(x, prepareData);
        }

        [TestMethod]
        public void TestIndexReturnResultNewestOrderList()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { user_id = 3 };
            int prepareData = 10;
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.Index() as ViewResult;
            var returnModel = result.Model as HomeModel;
            var x = returnModel.newestOrderList.Count;
            Assert.AreEqual(x, prepareData);
        }

        [TestMethod]
        public void TestIndexReturnResultProductInMonth()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { user_id = 3 };
            int prepareData = 1974;
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.Index() as ViewResult;
            var returnModel = result.Model as HomeModel;
            var x = returnModel.prodInMonth;
            Assert.AreEqual(x, prepareData);
        }

        [TestMethod]
        public void TestIndexReturnResultValueFlag()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { user_id = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.Index() as ViewResult;
            var returnModel = result.Model as HomeModel;
            var x = returnModel.valueFlag;
            Assert.AreEqual(x, true);
        }

        [TestMethod]
        public void TestIndexReturnResultDifferOrderMonth()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { user_id = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            decimal prepareData = 88.88888888888888888888888889M;
            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.Index() as ViewResult;
            var returnModel = result.Model as HomeModel;
            var x = returnModel.diffrentOrderMonth;
            Assert.AreEqual(x, prepareData);
        }

        [TestMethod]
        public void TestIndexReturnResultDifferValueMonth()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { user_id = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            decimal prepareData = 77.668727267508654967707980020M;
            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.Index() as ViewResult;
            var returnModel = result.Model as HomeModel;
            var x = returnModel.diffrentValueMonth;
            Assert.AreEqual(x, prepareData);
        }

        [TestMethod]
        public void TestIndexReturnResultDataLineChart()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            var sessionMock = new Mock<HttpSessionStateBase>();
            var userSession = new UserSession { user_id = 3 };
            sessionMock.Setup(n => n["USER_SESSION"]).Returns(userSession);
            httpContextMock.Setup(n => n.Session).Returns(sessionMock.Object);
            int prepareData = 9;
            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.Index() as ViewResult;
            var returnModel = result.Model as HomeModel;
            var x = returnModel.dataLineChartCurrentMonth.Count;
            Assert.AreEqual(x, prepareData);
        }

    }
}