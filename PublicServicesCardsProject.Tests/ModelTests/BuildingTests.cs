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
    public class BuildingTests
    {
        private Building building;

        [TestInitialize]
        public void SetUp()
        {
            building = new Building
            {
                AddressLine1 = "1",
                County = "C"
            };
        }

        [TestMethod]
        public void TestsForAddress2()
        {
            building.AddressLine2 = "2";
            var address = building.Address;
            address.Should().Be("1, 2, C");

            building.AddressLine3 = "3";
            address = building.Address;
            address.Should().Be("1, 2, 3, C");

            building.AddressLine4 = "4";
            address = building.Address;
            address.Should().Be("1, 2, 3, 4, C");
        }

        [TestMethod]
        public void TestForAddress3()
        {
            building.AddressLine3 = "3";
            var address = building.Address;
            address.Should().Be("1, 3, C");

            building.AddressLine4 = "4";
            address = building.Address;
            address.Should().Be("1, 3, 4, C");
        }

        [TestMethod]
        public void TestForAddress4()
        {
            building.AddressLine4 = "4";
            var address = building.Address;
            address.Should().Be("1, 4, C");
        }
    }
}
