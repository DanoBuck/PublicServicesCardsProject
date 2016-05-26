using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicServicesCardsProject.Migrations;

namespace PublicServicesCardsProject.Tests.MigrationTests
{
    [TestClass]
    public class InitialCreateTests
    {
        private InitialCreate initialCreate;

        [TestInitialize]
        public void SetUp()
        {
            initialCreate = new InitialCreate();
        }

        [TestMethod]
        public void TestUp()
        {
            initialCreate.Up();
        }

        [TestMethod]
        public void TestDown()
        {
            initialCreate.Down();
        }
    }
}
