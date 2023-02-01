using NUnit.Framework;
using Timesheet.API.Services;

namespace Timesheet.Tests
{
    public class AuthServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Login_ShouldReturnTrue()
        {
            //arrange(����������)

            var service = new AuthService();

            //act(����������)

            var result = service.Login();

            //assert(��������)
            Assert.IsTrue(result);
        }

        [Test]
        public void Login_ShouldReturnTrue()
        {
            //arrange(����������)

            //act(����������)

            var result = service.Login();

            //assert(��������)
            Assert.IsFalse(result);
        }
    }
}