using System.Collections.Generic;
using Timesheet.API.Models;

namespace Timesheet.API.Services
{
    public class TimesheetService
    {
        public bool TrackTime(TimeLog timeLog)
        {
            bool isValidHours = timeLog.WorkingHours > 0 && timeLog.WorkingHours <= 24;
            bool isValidLastName = !string.IsNullOrWhiteSpace(timeLog.LastName);

            if (!(isValidHours && isValidLastName))
                return false;

            if (!UserSession.Session.Contains(timeLog.LastName))
                return false;

            Timesheets.TimeLogs.Add(timeLog);

            return true;
        }
    }

    public static class Timesheets
    {
        public static List<TimeLog> TimeLogs { get; set; } = new List<TimeLog>();
    }
}
