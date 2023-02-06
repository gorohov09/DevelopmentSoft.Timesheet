using System.Collections.Generic;
using Timesheet.Domain.Interfaces;
using Timesheet.Domain.Models;

namespace Timesheet.App.Services
{
    public class TimesheetService : ITimesheetService
    {
        private readonly UserSession _userSession;
        private readonly ITimesheetRepository _timesheetRepository;

        public TimesheetService(UserSession userSession, ITimesheetRepository timesheetRepository)
        {
            _userSession = userSession;
            _timesheetRepository = timesheetRepository;
        }

        public bool TrackTime(TimeLog timeLog)
        {
            bool isValidHours = timeLog.WorkingHours > 0 && timeLog.WorkingHours <= 24;
            bool isValidLastName = !string.IsNullOrWhiteSpace(timeLog.LastName);

            if (!(isValidHours && isValidLastName))
                return false;

            if (!_userSession.Session.Contains(timeLog.LastName))
                return false;

            _timesheetRepository.Add(timeLog);

            return true;
        }
    }
}
