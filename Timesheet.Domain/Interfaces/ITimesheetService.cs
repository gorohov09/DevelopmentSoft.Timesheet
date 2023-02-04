using Timesheet.Domain.Models;

namespace Timesheet.Domain.Interfaces
{
    public interface ITimesheetService
    {
        bool TrackTime(TimeLog timeLog);
    }
}