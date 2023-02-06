﻿using System;
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
        private const decimal MAX_WORKING_HOURS_PER_DAY = 8m;

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

            //Используем переработку по дням
            var bill = CalculateBillWithRecyclingByDay(timeLogs, employee.Salary);

            return new EmployeeReport
            {
                LastName = employee.LastName,
                TimeLogs = timeLogs.ToList(),
                Bill = bill
            };
        }

        /// <summary>
        /// Подсчет денег, заработанных сотрудником с учетом переработки по месяцам
        /// </summary>
        /// <param name="timeLogs"></param>
        /// <param name="salary"></param>
        /// <returns></returns>
        private decimal CalculateBillWithRecyclingByMount(TimeLog[] timeLogs, decimal salary)
        {
            var bill = 0m;

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
                    bill += (recycleHours / MAX_WORKING_HOURS_PER_MONTH) * salary * 2 + salary;
                }
                else
                    bill += (hoursMonth / MAX_WORKING_HOURS_PER_MONTH) * salary;
            }

            return bill;
        }

        /// <summary>
        /// Подсчет денег, заработанных сотрудником с учетом переработки по дням
        /// </summary>
        /// <param name="timeLogs"></param>
        /// <param name="salary"></param>
        /// <returns></returns>
        private decimal CalculateBillWithRecyclingByDay(TimeLog[] timeLogs, decimal salary)
        {
            var bill = 0m;
            var totalHours = timeLogs.Sum(x => x.WorkingHours);

            var workingHoursGroupsByDay = timeLogs
                .GroupBy(x => x.Date.ToShortDateString());

            foreach (var workingLogsPerDay in workingHoursGroupsByDay)
            {
                int dayHours = workingLogsPerDay.Sum(x => x.WorkingHours);

                if (dayHours > MAX_WORKING_HOURS_PER_DAY)
                {
                    var overTime = dayHours - MAX_WORKING_HOURS_PER_DAY;

                    bill += MAX_WORKING_HOURS_PER_DAY / MAX_WORKING_HOURS_PER_MONTH * salary;
                    bill += overTime / MAX_WORKING_HOURS_PER_MONTH * salary * 2;
                }
                else
                    bill += dayHours / MAX_WORKING_HOURS_PER_MONTH * salary;
            }

            return bill;
        }
    }
}
