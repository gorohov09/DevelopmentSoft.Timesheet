using Timesheet.Domain.Models;

namespace Timesheet.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        StaffEmployee GetEmployee(string lastName);
    }
}
