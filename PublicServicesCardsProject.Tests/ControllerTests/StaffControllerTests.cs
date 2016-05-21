using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicServicesCardsProject.Controllers;
using PublicServicesCardsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PublicServicesCardsProject.Tests.ControllerTests
{
    [TestClass]
    public class StaffControllerTests
    {
        private StaffController controller;

        [TestInitialize]
        public void SetUp()
        {
            controller = new StaffController();
        }

        [TestMethod]
        public void TestIndex()
        {
            var result = controller.Index("PSC Gandon");
            result.Should().BeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public void TestDetails()
        {
            var result = controller.Details(2);
            result.Should().BeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public void TestDetailsWithNull()
        {
            var result = controller.Details(0);
            result.Should().BeOfType(typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void TestDetailsWithNullArg()
        {
            var result = controller.Details(null);
            result.Should().BeOfType(typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void TestCreateGet()
        {
            var result = controller.Create();
            result.Should().BeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public void TestCreatePost()
        {
            Staff staff = new Staff {
                StaffId = 1,
                FirstName = "Daniel",
                LastName = "Buckley",
                DateOfBirth = new DateTime(1995, 1, 9),
                EmailAddress = "DaniielBuckleyTy3@gmail.com",
                PPSN = "1234567Q",
                Salary = 45000,
                DeskNumber = 1,
                BuildingId = 1 // Gandon
            };
            var result = controller.Create(staff);
            result.Should().BeOfType(typeof(RedirectToRouteResult));

            var result2 = controller.DeleteConfirmed(1);
            result2.Should().BeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public void TestCreatePostException()
        {
            Staff staff = new Staff
            {
                StaffId = 1,
                FirstName = "Daniel",
                LastName = "Buckley",
                DateOfBirth = new DateTime(1995, 1, 9),
                EmailAddress = "DaniielBuckleyTy3@gmail.com",
                PPSN = "1234567Qasfssffsfsfsfsds",
                Salary = 45000,
                DeskNumber = 1,
                BuildingId = 1 // Gandon
            };
            var result = controller.Create(staff);
            result.Should().BeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public void TestEditGetWithNumber()
        {
            var result = controller.Edit(2);
            result.Should().BeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public void TestEditGetWithNull()
        {
            var result = controller.Edit(0);
            result.Should().BeOfType(typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void TestEditPost()
        {
            Staff staff = new Staff
            {
                StaffId = 2,
                FirstName = "Daniel",
                LastName = "Buckley",
                DateOfBirth = new DateTime(1995, 1, 9),
                EmailAddress = "DaniielBuckleyTy3@gmail.com",
                PPSN = "1234567Q",
                Salary = 45000,
                DeskNumber = 1,
                BuildingId = 1 // Gandon
            };
            var result = controller.Edit(staff);
            result.Should().BeOfType(typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void TestEditPostException()
        {
            Staff staff = new Staff
            {
                StaffId = 2,
                FirstName = "Daniel",
                LastName = "Buckley",
                DateOfBirth = new DateTime(1995, 1, 9),
                EmailAddress = "DaniielBuckleyTy3@gmail.com",
                PPSN = "1234567Qfdsffsffsfsfsfssfsf",
                Salary = 45000,
                DeskNumber = 1,
                BuildingId = 1 // Gandon
            };
            var result = controller.Edit(staff);
            result.Should().BeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public void TestDeleteWithNull()
        {
            var result = controller.Delete(null);
            result.Should().BeOfType(typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void TestDeleteWith0()
        {
            var result = controller.Delete(0);
            result.Should().BeOfType(typeof(HttpNotFoundResult));
        }


        [TestMethod]
        public void TestDeleteCorrectly()
        {
            var result = controller.Delete(2);
            result.Should().BeOfType(typeof(ViewResult));
        }

        [TestMethod]
        public void TestDispose()
        {
            controller.Dispose();
        }
    }
}
