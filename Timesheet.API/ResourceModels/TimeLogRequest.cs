using System;

namespace Timesheet.API.ResourceModels
{
    public class TimeLogRequest
    {
        public DateTime Date { get; set; }

        public int WorkingHours { get; set; }

        public string LastName { get; set; }

        public string Comment { get; set; }
    }
}
