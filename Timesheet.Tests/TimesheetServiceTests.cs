using NUnit.Framework;
using System;
using Timesheet.App.Services;
using Timesheet.Domain.Interfaces;
using Timesheet.Domain.Models;

namespace Timesheet.Tests
{
    public class TimesheetServiceTests
    {
        [Test]
        public void TrackTime_ShouldReturnTrue()
        {
            //arrange
            var userSession = new UserSession();
            var expectedLastName = "TestUser";
            userSession.Session.Add(expectedLastName);

            var timeLog = new TimeLog
            {
                WorkingHours = 4,
                Date = new DateTime(),
                LastName = expectedLastName,
                Comment = "Решал задачки"
            };

            var service = new TimesheetService(userSession);

            var result = service.TrackTime(timeLog);

            Assert.IsTrue(result);
        }

        //Нельзя залогировать больше 24 часов или меньше 0 часов
        //Нельзя залогировать с фамилией человека, которого нет в данных

        [TestCase("Егоров", 13)]
        [TestCase("Иванов", 25)]
        [TestCase("Иванов", 0)]
        [TestCase("Егоров", 0)]
        [TestCase(null, 0)]
        [TestCase("", 0)]
        public void TrackTime_ShouldReturnFalse(string lastName, int workingHours)
        {
            var userSession = new UserSession();
            var timeLog = new TimeLog
            {
                WorkingHours = workingHours,
                Date = new DateTime(),
                LastName = lastName,
                Comment = "Решал задачки"
            };

            var service = new TimesheetService(userSession);

            var result = service.TrackTime(timeLog);

            Assert.IsFalse(result);
        }
    }
}
