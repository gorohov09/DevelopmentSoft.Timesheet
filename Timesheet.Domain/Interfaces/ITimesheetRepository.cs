using System.Collections.Generic;
using Timesheet.Domain.Models;

namespace Timesheet.Domain.Interfaces
{
    public interface ITimesheetRepository
    {
        TimeLog[] GetTimeLogs(string lastName);
    }
}
