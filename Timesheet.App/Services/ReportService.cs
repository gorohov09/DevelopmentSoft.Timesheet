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
            var bill = 0m;

            //Логику для подсчета заработанных денег сотрудником в определенный период вынести в отдельный метод,
            //а может быть и подумать как это пихнуть в Domain

            var timeLogsByMounth = timeLogs.GroupBy(x => new
            {
                x.Date.Year, //Группируем по месяцу и году, так как отчет может быть за >12 месяцев 
                x.Date.Month,
            });

            foreach (var mounth in timeLogsByMounth)
            {
                var hoursMonth = mounth.Sum(x => x.WorkingHours);
                if (hoursMonth > MAX_WORKING_HOURS_PER_MONTH) //Если переработка
                {
                    var recycleHours = hoursMonth - MAX_WORKING_HOURS_PER_MONTH; //Переработанные часы
                    bill += (recycleHours / MAX_WORKING_HOURS_PER_MONTH) * employee.Salary * 2 + employee.Salary;
                }
                else
                    bill += (hoursMonth / MAX_WORKING_HOURS_PER_MONTH) * employee.Salary;
            }

            return new EmployeeReport
            {
                LastName = employee.LastName,
                TimeLogs = timeLogs.ToList(),
                Bill = bill
            };
        }
    }
}
