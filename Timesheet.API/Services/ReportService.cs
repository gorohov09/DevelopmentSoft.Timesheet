using System;
using Timesheet.API.Models;

namespace Timesheet.API.Services
{
    public class ReportService
    {
        public EmployeeReport GetEmployeeReport(string lastName)
        {
            return new EmployeeReport();
        }
    }
}
