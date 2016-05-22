using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicServicesCardsProject.Controllers;
using System.Web.Mvc;

namespace PublicServicesCardsProject.Tests.ControllerTests
{
    [TestClass]
    public class ManageControllerTests
    {
        private ManageController controller;

        [TestInitialize]
        public void SetUp()
        {
            controller = new ManageController();
        }

        [TestMethod]
        public void TestPhoneNumberGet()
        {
            var result = controller.AddPhoneNumber();
            result.Should().BeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public void TestChangePasswordGet()
        {
            var result = controller.ChangePassword();
            result.Should().BeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public void TestSetPasswordGet()
        {
            var result = controller.SetPassword();
            result.Should().BeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public void TestLinkLoginPost()
        {
            controller.Dispose();
        }
    }
}