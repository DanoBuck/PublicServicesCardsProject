using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicServicesCardsProject.Models;

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
