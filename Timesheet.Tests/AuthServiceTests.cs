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
            //arrange(Подготовка)

            var service = new AuthService();

            //act(Выполнение)

            var result = service.Login();

            //assert(Проверка)
            Assert.IsTrue(result);
        }

        [Test]
        public void Login_ShouldReturnTrue()
        {
            //arrange(Подготовка)

            //act(Выполнение)

            var result = service.Login();

            //assert(Проверка)
            Assert.IsFalse(result);
        }
    }
}