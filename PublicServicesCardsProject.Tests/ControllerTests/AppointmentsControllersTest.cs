using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicServicesCardsProject.Controllers;
using System.Web.Mvc;

namespace PublicServicesCardsProject.Tests.ControllerTests
{
    [TestClass]
    public class AppointmentsControllersTest
    {
        private AppointmentsController controller;

        [TestInitialize]
        public void SetUp()
        {
            controller = new AppointmentsController();
        }

        [TestMethod]
        public void TestDetailsWithNull()
        {
            var result = controller.Details(0);
            result.Should().BeOfType(typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void TestDetails()
        {
            var result = controller.Details(5);
            result.Should().BeOfType(typeof(ViewResult));
        }
    }
}
