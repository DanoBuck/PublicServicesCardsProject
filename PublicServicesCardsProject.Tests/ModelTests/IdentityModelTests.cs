using FluentAssertions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public class IdentityModelTests
    {
        [TestMethod]
        public void TestApplicationUser()
        {
            ApplicationUser user = new ApplicationUser
            {
                StaffId = 1,
                CustomerId = 1,
                Staff = new Staff(),
                Customers = new Customer()
            };
            user.Should().NotBeNull();
        }
    }
}
