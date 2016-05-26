using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicServicesCardsProject.Models;
using System;

namespace PublicServicesCardsProject.Tests.ModelTests
{
    [TestClass]
    public class CustomerTests
    {
        private Customer customer;

        [TestInitialize]
        public void SetUp()
        {
            customer = new Customer
            {
                CustomerId = 1,
                FirstName = "Adam",
                LastName = "Buckley",
                DateOfBirth = new DateTime(1996, 12, 2),
                EmailAddress = "Adam@gmail.com",
                PPSN = "1234567S",
                CivilStatus = "Single"
            };
        }

        [TestMethod]
        public void TestFullName()
        {
            var name = customer.Name;
            name.Should().Be("Adam Buckley");
        }

        [TestMethod]
        public void TestDateOfBirth()
        {
            var age = customer.Age;
            age.Should().Be(19);
        }

        [TestMethod]
        public void TestDateOfBirthForLeapYear()
        {
            customer.DateOfBirth = new DateTime(1920, 2, 29);
            var age = customer.Age;
            age.Should().Be(96);
        }
    }
}
