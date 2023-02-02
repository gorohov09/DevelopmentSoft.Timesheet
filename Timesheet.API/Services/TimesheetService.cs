using System.Collections.Generic;
using Timesheet.API.Models;

namespace Timesheet.API.Services
{
    public class TimesheetService
    {
        public bool TrackTime(TimeLog timeLog)
        {


            Timesheets.TimeLogs.Add(timeLog);

            return true;
        }
    }

    public static class Timesheets
    {
        public static List<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
    }
}
