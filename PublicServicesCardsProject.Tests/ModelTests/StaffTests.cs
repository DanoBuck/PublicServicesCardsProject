using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicServicesCardsProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicServicesCardsProject.Tests.ModelTests
{
    [TestClass]
    public class StaffTests
    {
        private Staff staff;

        [TestInitialize]
        public void SetUp()
        {
            staff = new Staff();
        }

        [TestMethod]
        public void TestFullName()
        {
            staff.FirstName = "Daniel";
            staff.LastName = "Buckley";
            var name = staff.Name;
            name.Should().Be("Daniel Buckley");
        }

        [TestMethod]
        public void TestDateOfBirth()
        {
            staff.DateOfBirth = new DateTime(1996, 12, 2);
            var age = staff.Age;
            age.Should().Be(19);
        }

        [TestMethod]
        public void TestDateOfBirthForLeapYear()
        {
            staff.DateOfBirth = new DateTime(1920, 2, 29);
            var age = staff.Age;
            age.Should().Be(96);
        }
    }
}
