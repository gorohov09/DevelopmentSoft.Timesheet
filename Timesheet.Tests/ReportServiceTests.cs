using Moq;
using NUnit.Framework;
using System;
using Timesheet.App.Services;
using Timesheet.Domain.Interfaces;
using Timesheet.Domain.Models;

namespace Timesheet.Tests
{
    public class ReportServiceTests
    {
        [Test]
        public void GetEmployeeReport_ShouldReturnReport()
        {
            var timesheetRepositoryMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();

            var expectedLastName = "Иванов";
            var expectedTotal = 8750m;

            timesheetRepositoryMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new [] { 
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Date = DateTime.Now,
                        WorkingHours = 8,
                        Comment = "Решал задачу"
                    },
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Date = DateTime.Now.AddDays(-1),
                        WorkingHours = 8,
                        Comment = "Составлял отчет"
                    },
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Date = DateTime.Now.AddDays(-7),
                        WorkingHours = 4,
                        Comment = "Был на встрече"
                    }
                })
                .Verifiable();

            employeeRepositoryMock
                .Setup(x => x.GetEmployee(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new StaffEmployee
                {
                    LastName = expectedLastName,
                    Salary = 70000
                })
                .Verifiable();

            //arrange
            var service = new ReportService(timesheetRepositoryMock.Object, employeeRepositoryMock.Object);

            //act
            var result = service.GetEmployeeReport(expectedLastName);

            //assert
            timesheetRepositoryMock.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
        }

        [Test]
        public void GetEmployeeReportWithRecycling_ShouldReturnReport()
        {
            var timesheetRepositoryMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();

            var expectedLastName = "Иванов";
            var expectedTotal = 105750m;

            timesheetRepositoryMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() =>
                {
                    TimeLog[] timeLogs = new TimeLog[35];
                    DateTime dateTime = new DateTime(2020, 11, 1);
                    timeLogs[0] = new TimeLog
                    {
                        LastName = expectedLastName,
                        Comment = Guid.NewGuid().ToString(),
                        Date = dateTime,
                        WorkingHours = 9
                    };

                    for (int i = 1; i < timeLogs.Length; i++)
                    {
                        dateTime = dateTime.AddDays(1);
                        timeLogs[i] = new TimeLog
                        {
                            Date = dateTime,
                            Comment = Guid.NewGuid().ToString(),
                            LastName = expectedLastName,
                            WorkingHours = 8
                        };
                    }

                    return timeLogs;
                })
                .Verifiable();

            employeeRepositoryMock
                .Setup(x => x.GetEmployee(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new StaffEmployee
                {
                    LastName = expectedLastName,
                    Salary = 60000
                })
                .Verifiable();

            //arrange
            var service = new ReportService(timesheetRepositoryMock.Object, employeeRepositoryMock.Object);

            //act
            var result = service.GetEmployeeReport(expectedLastName);

            //assert
            timesheetRepositoryMock.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
        }

        [Test]
        public void GetEmployeeReportWithoutTimeLogs_ShouldReturnReport()
        {
            var timesheetRepositoryMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();

            var expectedLastName = "Иванов";
            var expectedTotal = 0m;
            var expectedTotalHours = 0;

            timesheetRepositoryMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new TimeLog[0])
                .Verifiable();

            employeeRepositoryMock
                .Setup(x => x.GetEmployee(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new StaffEmployee
                {
                    LastName = expectedLastName,
                    Salary = 60000
                })
                .Verifiable();

            //arrange
            var service = new ReportService(timesheetRepositoryMock.Object, employeeRepositoryMock.Object);

            //act
            var result = service.GetEmployeeReport(expectedLastName);

            //assert
            timesheetRepositoryMock.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHours, result.TotalHours);
        }

        [Test]
        public void GetEmployeeReportForOneDay_ShouldReturnReport()
        {
            var timesheetRepositoryMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();

            var expectedLastName = "Иванов";
            var expectedTotal = 8m /160m * 70000m;
            var expectedTotalHours = 0;

            timesheetRepositoryMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new []
                {
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Comment = Guid.NewGuid().ToString(),
                        Date = DateTime.Now.AddDays(-1),
                        WorkingHours = 8
                    }
                })
                .Verifiable();

            employeeRepositoryMock
                .Setup(x => x.GetEmployee(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new StaffEmployee
                {
                    LastName = expectedLastName,
                    Salary = 70000
                })
                .Verifiable();

            //arrange
            var service = new ReportService(timesheetRepositoryMock.Object, employeeRepositoryMock.Object);

            //act
            var result = service.GetEmployeeReport(expectedLastName);

            //assert
            timesheetRepositoryMock.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHours, result.TotalHours);
        }
    }
}
