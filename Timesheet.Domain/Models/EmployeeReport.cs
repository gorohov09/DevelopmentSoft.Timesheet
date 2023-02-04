using System;
using System.Collections.Generic;

namespace Timesheet.Domain.Models
{
    /*
     Отчет по сотруднику: [Имя сотрудника] за период с [дата начала] по [дата окончания]
    10.10.2020, 8 часов, исправлял работу модуля отчетов
    11.10.2020, 8 часов, разработка новой функциональности модуля интеграции
    12.10.2020, 10 часов, срочные исправления модуля интеграции
    Итого: 26 часов, заработано: 2000 руб
     */

    public class EmployeeReport
    {
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Дата начала периода
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания периода
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Список тайм-логов сотрудника
        /// </summary>
        public List<TimeLog> TimeLogs { get; set; }

        /// <summary>
        /// Кол-во часов, отработанных сотрудником
        /// </summary>
        public int TotalHours { get; set; }

        /// <summary>
        /// Кол-во денег, заработанных сотрудником
        /// </summary>
        public decimal Bill { get; set; }
    }
}
