using System.Collections.Generic;

namespace Timesheet.Domain.Interfaces
{
    public interface IAuthService
    {
        List<string> Employees { get; }

        bool Login(string lastName);
    }
}