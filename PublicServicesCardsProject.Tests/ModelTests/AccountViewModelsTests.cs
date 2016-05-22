using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicServicesCardsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PublicServicesCardsProject.Tests.ModelTests
{
    [TestClass]
    public class AccountViewModelsTests
    {
        [TestMethod]
        public void TestExternalLoginConfirmationViewModel()
        {
            ExternalLoginConfirmationViewModel viewModel = new ExternalLoginConfirmationViewModel
            {
                Email = "Email@Email.ie"
            };
            viewModel.Should().NotBe(null);
        }

        [TestMethod]
        public void TestExternalLoginListViewModel()
        {
            ExternalLoginListViewModel viewModel = new ExternalLoginListViewModel
            {
                ReturnUrl = "ReturnUrl"
            };
            viewModel.Should().NotBeNull();
        }

        [TestMethod]
        public void TestSendCodeViewModel()
        {
            ICollection<SelectListItem> select = new List<SelectListItem>();
            SendCodeViewModel viewModel = new SendCodeViewModel
            {
                SelectedProvider = "Provider",
                Providers = select,
                ReturnUrl = "UrlReturned",
                RememberMe = true
            };
            viewModel.Should().NotBeNull();
        }

        [TestMethod]
        public void VerifyCodeViewModel()
        {
            VerifyCodeViewModel viewModel = new VerifyCodeViewModel
            {
                Code = "MyCode",
                ReturnUrl = "ReturnUrl",
                RememberBrowser = true,
                RememberMe = true,
                Provider = "Provider"
            };
            viewModel.Should().NotBeNull();
        }

        [TestMethod]
        public void TestForgotViewModel()
        {
            ForgotViewModel viewModel = new ForgotViewModel
            {
                Email = "Email@Email.ie"
            };
            viewModel.Should().NotBeNull();
        }

        [TestMethod]
        public void TestRegisterViewModel ()
        {
            RegisterViewModel viewModel = new RegisterViewModel
            {
                Email = "Email@Email.ie",
                Password = "PassWord1'",
                ConfirmPassword = "PassWord1'",
                Customers = new Customer(),
                Staff = new Staff()
            };
            viewModel.Should().NotBeNull();
        }

        [TestMethod]
        public void TestResetPasswordViewModel()
        {
            ResetPasswordViewModel viewModel = new ResetPasswordViewModel
            {
                Email = "Email@Email.ie",
                Password = "PassWord1'",
                ConfirmPassword = "PassWord1'",
                Code = "MyCode"
            };
            viewModel.Should().NotBeNull();
        }

        [TestMethod]
        public void TestForgotPasswordViewModel()
        {
            ForgotPasswordViewModel viewModel = new ForgotPasswordViewModel
            {
                Email = "Email@Email.ie"
            };
            viewModel.Should().NotBeNull();
        }

        [TestMethod]
        public void TestUserRoleViewModel()
        {
            ICollection<string> myList = new List<string>();
            UserRoleViewModel viewModel = new UserRoleViewModel
            {
                Roles = myList,
                EmailAddress = "Email@Email.ie"
            };
            viewModel.Should().NotBe(null);
        }
    }
}
