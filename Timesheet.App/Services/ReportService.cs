using System;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Domain.Interfaces;
using Timesheet.Domain.Models;

namespace Timesheet.App.Services
{
    /*
     * Формула для расчета зарплаты по дням: bill = (hours / 160) * salary
     * Где hours - общее кол-во отработанных часов в временном промежутке, salary - зарплата сотрудника
     */

    public class ReportService
    {
        private const decimal MAX_WORKING_HOURS_PER_MONTH = 160m;

        private readonly ITimesheetRepository _timesheetRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ReportService(ITimesheetRepository timesheetRepository, IEmployeeRepository employeeRepository)
        {
            _timesheetRepository = timesheetRepository;
            _employeeRepository = employeeRepository;
        }

        public EmployeeReport GetEmployeeReport(string lastName)
        {
            var employee = _employeeRepository.GetEmployee(lastName);
            var timeLogs = _timesheetRepository.GetTimeLogs(employee.LastName);

            return new EmployeeReport
            {
                LastName = employee.LastName,
                TimeLogs = timeLogs.ToList(),
                Bill = (timeLogs.Sum(x => x.WorkingHours) / MAX_WORKING_HOURS_PER_MONTH) * employee.Salary
            };
        }
    }
}
