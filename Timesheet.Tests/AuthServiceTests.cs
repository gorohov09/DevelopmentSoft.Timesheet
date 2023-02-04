using NUnit.Framework;
using Timesheet.App.Services;

namespace Timesheet.Tests
{
    public class AuthServiceTests
    {
        [TestCase("Иванов")]
        [TestCase("Петров")]
        [TestCase("Сидоров")]
        public void Login_ShouldReturnTrue(string lastName)
        {
            //arrange(Подготовка)
            var userSession = new UserSession();
            var service = new AuthService(userSession); 

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
            var userSession = new UserSession();
            var service = new AuthService(userSession);

            //act(Выполнение)

            var result = service.Login(lastName);

            //assert(Проверка)
            Assert.IsFalse(result);
        }
    }
}