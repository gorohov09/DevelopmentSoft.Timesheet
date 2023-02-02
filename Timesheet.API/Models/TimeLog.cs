﻿using System;

namespace Timesheet.API.Models
{
    public class TimeLog
    {
        public DateTime Date { get; set; }

        public int WorkingHours { get; set; }

        public string LastName { get; set; }
    }
}