using System;
using System.Collections.Generic;
using System.IO;
using Timesheet.Domain.Interfaces;
using Timesheet.Domain.Models;

namespace Timesheet.DataAccess.CSV
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private const string DELIMETER = ";";
        private const string PATH = "..\\Timesheet.DataAccess.CSV\\Data\\timesheet.csv";

        public void Add(TimeLog timeLog)
        {
            string dataRow = $"{timeLog.LastName}{DELIMETER}" +
                $"{timeLog.Date}{DELIMETER}" +
                $"{timeLog.Comment}{DELIMETER}" +
                $"{timeLog.WorkingHours}\n";

            File.AppendAllText(PATH, dataRow);
        }

        public TimeLog[] GetTimeLogs(string lastName)
        {
            var timeLogs = new List<TimeLog>();
            var data = File.ReadAllText(PATH);

            foreach (var dataRow in data.Split('\n'))
            {
                var lineWords = dataRow.Split(';');

                if (lineWords[0] == lastName)
                {
                    timeLogs.Add(new TimeLog
                    {
                        LastName = lineWords[0],
                        Date = DateTime.Parse(lineWords[1]),
                        Comment = lineWords[2],
                        WorkingHours = int.Parse(lineWords[3]),
                    });
                }
            }

            return timeLogs.ToArray();
        }
    }
}
