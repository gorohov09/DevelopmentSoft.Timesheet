using NUnit.Framework;
using System;
using Timesheet.API.Models;
using Timesheet.API.Services;

namespace Timesheet.Tests
{
    public class TimesheetServiceTests
    {
        [Test]
        public void TrackTime_ShouldReturnTrue()
        {
            //arrange
            var timeLog = new TimeLog
            {
                WorkingHours = 4,
                Date = new DateTime(),
                LastName = ""
            };

            var service = new TimesheetService();

            var result = service.TrackTime(timeLog);

            Assert.IsTrue(result);
        }

        [Test]
        public void TrackTime_ShouldReturnFalse()
        {
            var timeLog = new TimeLog
            {
                WorkingHours = 4,
                Date = new DateTime(),
                LastName = ""
            };

            var service = new TimesheetService();

            var result = service.TrackTime(timeLog);

            Assert.IsFalse(result);
        }
    }
}
