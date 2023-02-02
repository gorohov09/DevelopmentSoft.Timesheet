using NUnit.Framework;
using Timesheet.API.Services;

namespace Timesheet.Tests
{
    public class ReportServiceTests
    {
        [Test]
        public void GetEmployeeReport_ShouldReturnReport()
        {
            //arrange
            var expectedLastName = "Иванов";
            var service = new ReportService();
            var expectedTotal = 100m;

            //act
            var result = service.GetEmployeeReport(expectedLastName);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
        }
    }
}
