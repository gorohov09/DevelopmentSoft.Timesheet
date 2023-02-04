using System;
using Timesheet.Domain.Models;

namespace Timesheet.App.Services
{
    public class ReportService
    {
        public EmployeeReport GetEmployeeReport(string lastName)
        {
            return new EmployeeReport();
        }
    }
}
