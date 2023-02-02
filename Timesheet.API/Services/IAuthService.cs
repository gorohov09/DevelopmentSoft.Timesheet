using System.Collections.Generic;

namespace Timesheet.API.Services
{
    public interface IAuthService
    {
        List<string> Employees { get; }

        bool Login(string lastName);
    }
}