using FluentAssertions;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
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
    public class ManageViewModelTests
    {
        [TestMethod]
        public void TestIndexViewModel()
        {
            IList<UserLoginInfo> logins = new List<UserLoginInfo>();
            IndexViewModel viewModel = new IndexViewModel
            {
                HasPassword = true,
                Logins = logins,
                PhoneNumber = "0851234567",
                TwoFactor = true,
                BrowserRemembered = true
            };
            viewModel.Should().NotBeNull();
        }

        [TestMethod]
        public void TestManageLoginsViewModel()
        {
            IList<UserLoginInfo> logins = new List<UserLoginInfo>();
            IList<AuthenticationDescription> otherLogins = new List<AuthenticationDescription>();
            ManageLoginsViewModel viewModel = new ManageLoginsViewModel
            {
                CurrentLogins = logins,
                OtherLogins = otherLogins
            };
            viewModel.Should().NotBeNull();
        }

        [TestMethod]
        public void TestFactorViewModel()
        {
            FactorViewModel viewModel = new FactorViewModel
            {
                Purpose = "ToFactor"
            };
            viewModel.Should().NotBeNull();
        }

        [TestMethod]
        public void TestSetPasswordViewModel()
        {
            SetPasswordViewModel viewModel = new SetPasswordViewModel
            {
                NewPassword = "PassWord1'",
                ConfirmPassword = "PassWord1'"
            };
            viewModel.Should().NotBeNull();
        }

        [TestMethod]
        public void TestChangePasswordViewModel()
        {
            ChangePasswordViewModel viewModel = new ChangePasswordViewModel
            {
                OldPassword = "PassWord1'",
                NewPassword = "Password2'",
                ConfirmPassword = "Password2'"
            };
            viewModel.Should().NotBeNull();
        }

        [TestMethod]
        public void TestAddPhoneNumberViewModel()
        {
            AddPhoneNumberViewModel viewModel = new AddPhoneNumberViewModel
            {
                Number = "0851234567"
            };
            viewModel.Should().NotBeNull();
        }

        [TestMethod]
        public void TestVerifyPhoneNumberViewModel()
        {
            VerifyPhoneNumberViewModel viewModel = new VerifyPhoneNumberViewModel
            {
                Code = "MyCode",
                PhoneNumber = "0851234567"
            };
            viewModel.Should().NotBeNull();
        }

        [TestMethod]
        public void TestConfigureTwoFactorViewModel()
        {
            ICollection<SelectListItem> myList = new List<SelectListItem>();
            ConfigureTwoFactorViewModel viewModel = new ConfigureTwoFactorViewModel
            {
                SelectedProvider = "MyProvider",
                Providers = myList
            };
            viewModel.Should().NotBeNull();
        }
    }
}
