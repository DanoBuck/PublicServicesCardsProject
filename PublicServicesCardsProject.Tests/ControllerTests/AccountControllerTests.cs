using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicServicesCardsProject.Controllers;
using PublicServicesCardsProject.Models;
using System.Web.Mvc;

namespace PublicServicesCardsProject.Tests.ControllerTests
{
    [TestClass]
    public class AccountControllerTests
    {
        private AccountController controller;

        [TestInitialize]
        public void SetUp()
        {
            controller = new AccountController();
        }

        [TestMethod]
        public void TestLoginGet()
        {
            var result = controller.Login("Url");
            result.Should().BeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public void TestLoginPost()
        {
            LoginViewModel model = new LoginViewModel
            {
                Email = "Email@Email.com",
                Password = "PassWord1'",
                RememberMe = true
            };
            controller.Login(model, "Url");
        }
    }
}
