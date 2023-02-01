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

        [TestCase("Иванов")]
        [TestCase("Петров")]
        [TestCase("Сидоров")]
        public void Login_ShouldReturnTrue(string lastName)
        {
            //arrange(Подготовка)
            var service = new AuthService();

            //act(Выполнение)

            var result = service.Login(lastName);

            //assert(Проверка)
            Assert.IsTrue(result);
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("TestUser")]
        public void Login_ShouldReturnFalse(string lastName)
        {
            //arrange(Подготовка)
            var service = new AuthService();

            //act(Выполнение)

            var result = service.Login(lastName);

            //assert(Проверка)
            Assert.IsFalse(result);
        }
    }
}