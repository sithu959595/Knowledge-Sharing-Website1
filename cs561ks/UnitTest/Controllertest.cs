using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using cs561ks;
using MongoDBtest.Controllers;
using MongoDB.Driver;
using MongoDBtest.Models;
using Moq;
using System.IO;
using System.Reflection;
using System.Web.SessionState;
using System.Web.Routing;
using System.Collections.Specialized;

namespace UnitTest
{
    public static class MvcMockHelpers
    {
        public static HttpContextBase FakeHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();

            context.Expect(ctx => ctx.Request).Returns(request.Object);
            context.Expect(ctx => ctx.Response).Returns(response.Object);
            context.Expect(ctx => ctx.Session).Returns(session.Object);
            context.Expect(ctx => ctx.Server).Returns(server.Object);

            return context.Object;
        }
        public static HttpContextBase FakeHttpContext(string url)
        {
            HttpContextBase context = FakeHttpContext();
            context.Request.SetupRequestUrl(url);
            return context;
        }
        public static void SetFakeControllerContext(this Controller controller)
        {
            var httpContext = FakeHttpContext();
            ControllerContext context = new ControllerContext(new RequestContext(httpContext, new RouteData()), controller);
            controller.ControllerContext = context;
        }
        static string GetUrlFileName(string url)
        {
            if (url.Contains("?"))
                return url.Substring(0, url.IndexOf("?"));
            else
                return url;
        }
        static NameValueCollection GetQueryStringParameters(string url)
        {
            if (url.Contains("?"))
            {
                NameValueCollection parameters = new NameValueCollection();

                string[] parts = url.Split("?".ToCharArray());
                string[] keys = parts[1].Split("&".ToCharArray());

                foreach (string key in keys)
                {
                    string[] part = key.Split("=".ToCharArray());
                    parameters.Add(part[0], part[1]);
                }

                return parameters;
            }
            else
            {
                return null;
            }
        }

        public static void SetHttpMethodResult(this HttpRequestBase request, string httpMethod)
        {
            Mock.Get(request)
                .Expect(req => req.HttpMethod)
                .Returns(httpMethod);
        }

        public static void SetupRequestUrl(this HttpRequestBase request, string url)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            if (!url.StartsWith("~/"))
                throw new ArgumentException("Sorry, we expect a virtual url starting with \"~/\".");

            var mock = Mock.Get(request);

            mock.Expect(req => req.QueryString)
                .Returns(GetQueryStringParameters(url));
            mock.Expect(req => req.AppRelativeCurrentExecutionFilePath)
                .Returns(GetUrlFileName(url));
            mock.Expect(req => req.PathInfo)
                .Returns(string.Empty);
        }
    }
    [TestClass]
    public class Controllertest
    {
        [TestMethod]
        public void Index()
        {
            //Arrange
            HomeController homeController = new HomeController();

            //Act
            ViewResult result = homeController.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Register()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Register() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void admin()
        {
            UserController controller = new UserController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void login_success()
        {
            HomeController controller = new HomeController();

            /*var user = new FormCollection();
            user.Add("username", "test");
            user.Add("password", "test");*/

            bool result = controller.logincheck("test", "test");
            //bool result = MongoHelper.logincheck("test", "test");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void login_fail()
        {
            HomeController controller = new HomeController();

            bool result = controller.logincheck("asdfwef", "asdfwerq");

            Assert.IsFalse(result);
        }

        [TestInitialize]
        public void TestSetup()
        {
            // We need to setup the Current HTTP Context as follows:            

            // Step 1: Setup the HTTP Request
            var httpRequest = new HttpRequest("", "http://localhost/Home/Profile", "");

            // Step 2: Setup the HTTP Response
            var httpResponce = new HttpResponse(new StringWriter());

            // Step 3: Setup the Http Context
            var httpContext = new HttpContext(httpRequest, httpResponce);
            var sessionContainer =
                new HttpSessionStateContainer("id",
                                               new SessionStateItemCollection(),
                                               new HttpStaticObjectsCollection(),
                                               10,
                                               true,
                                               HttpCookieMode.AutoDetect,
                                               SessionStateMode.InProc,
                                               false);
            httpContext.Items["AspSession"] =
                typeof(HttpSessionState)
                .GetConstructor(
                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                    null,
                                    CallingConventions.Standard,
                                    new[] { typeof(HttpSessionStateContainer) },
                                    null)
                .Invoke(new object[] { sessionContainer });

            // Step 4: Assign the Context
            HttpContext.Current = httpContext;
        }

        [TestMethod]
        public void sessiontest()
        {
            // Arrange
            var itemValue = "test";
            var itemKey = "Username";
            // Act
            HttpContext.Current.Session.Add(itemKey, itemValue);
            // Assert
            Assert.AreEqual(HttpContext.Current.Session[itemKey], itemValue);
        }

        [TestMethod]
        public void profile()
        {
            // Arrange
            var itemValue = "test";
            var itemKey = "Username";
            HomeController controller = new HomeController();

            FormCollection collection = new FormCollection();

            ActionResult res = controller.showProfile("test");



            // Assert
            Assert.IsNotNull(res);
            //Assert.IsNotNull(res);
            //Assert.AreEqual(HttpContext.Current.Session[itemKey], itemValue);

        }

        [TestMethod]
        public void posts()
        {
            HomeController controller = new HomeController();

            ActionResult res = controller.ShowPosts("test") as ViewResult;

            Assert.IsNotNull(res);

        }
        [TestMethod]
        public void integration()
        {
            HomeController home_controller = new HomeController();

            UserController user_controller = new UserController();

            ViewResult result = home_controller.Index() as ViewResult;

            if (result != null)
                System.Diagnostics.Debug.WriteLine("Index page works well");
            else
                System.Diagnostics.Debug.WriteLine("Index page is broken");

            result = home_controller.Register() as ViewResult;

            if (result != null)
                System.Diagnostics.Debug.WriteLine("Register page works well");
            else
                System.Diagnostics.Debug.WriteLine("Register page is broken");

            bool res = home_controller.logincheck("test", "test");
            bool res2 = home_controller.logincheck("asdfawer", "aserq");

            if (res == true && res2 == false)
                System.Diagnostics.Debug.WriteLine("Login system works well");
            else
                System.Diagnostics.Debug.WriteLine("Login system is broken");

            // Arrange
            var itemValue = "test";
            var itemKey = "Username";
            // Act
            HttpContext.Current.Session.Add(itemKey, itemValue);
            if (HttpContext.Current.Session[itemKey] == itemValue)
                System.Diagnostics.Debug.WriteLine("Session test success");
            else
                System.Diagnostics.Debug.WriteLine("Session test fail");

            result = null;
            result = home_controller.showProfile("test") as ViewResult;

            if (result != null)
                System.Diagnostics.Debug.WriteLine("Profile page works well");
            else
                System.Diagnostics.Debug.WriteLine("Profile page is broken");

            result = null;
            result = home_controller.ShowPosts("test") as ViewResult;

            if (result != null)
                System.Diagnostics.Debug.WriteLine("Post works well");
            else
                System.Diagnostics.Debug.WriteLine("Post is broken");

        }
    }
}
