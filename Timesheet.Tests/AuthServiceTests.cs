using NUnit.Framework;
using Timesheet.API.Services;

namespace Timesheet.Tests
{
    public class AuthServiceTests
    {
        [TestCase("������")]
        [TestCase("������")]
        [TestCase("�������")]
        public void Login_ShouldReturnTrue(string lastName)
        {
            //arrange(����������)
            var service = new AuthService();

            //act(����������)

            var result = service.Login(lastName);

            //assert(��������)
            Assert.IsTrue(result);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("TestUser")]
        public void Login_ShouldReturnFalse(string lastName)
        {
            //arrange(����������)
            var service = new AuthService();

            //act(����������)

            var result = service.Login(lastName);

            //assert(��������)
            Assert.IsFalse(result);
        }
    }
}