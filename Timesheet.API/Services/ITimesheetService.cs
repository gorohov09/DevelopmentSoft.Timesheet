using Timesheet.API.Models;

namespace Timesheet.API.Services
{
    public interface ITimesheetService
    {
        bool TrackTime(TimeLog timeLog);
    }
}