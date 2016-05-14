﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicServicesCardsProject.Controllers;
using FluentAssertions;
using System.Web.Mvc;
using System.Net;
using PublicServicesCardsProject.Models;

namespace PublicServicesCardsProject.Tests.ControllerTests
{
    [TestClass]
    public class BuildingContollerTests
    {
        private BuildingsController controller;

        [TestInitialize]
        public void SetUp()
        {
            controller = new BuildingsController();
        }

        [TestMethod]
        public void TestIndex()
        {
            var result = controller.Index() as ViewResult;

            result.Should().NotBeNull();
        }

        [TestMethod]
        public void DetailsTestIfNullPassed()
        {
            var result = controller.Details(null);

            result.Should().NotBe(null);
        }

        [TestMethod]
        public void TestDetailsIfBuildingNotFound()
        {
            var result = controller.Details(0);
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void TestDetailsIfBuildingFound()
        {
            var result = controller.Details(1);
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void TestCreateGet()
        {
            var result = controller.Create();
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void TestCreatePostCorrect()
        {
            Building building = new Building {
                SafeOffice = "Intreo Centre Swords",
                AddressLine1 = "Mainscourt",
                AddressLine2 = "23 Main Street",
                AddressLine3 = "Swords",
                //AddressLine4 = "Tallaght",
                County = "Dublin",
                Phone = "(01) 4629496"
            };
            var result = controller.Create(building);
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void TestEditWithNull()
        {
            var result = controller.Edit(0);
            result.Should().NotBe(null);
        }
    }
}