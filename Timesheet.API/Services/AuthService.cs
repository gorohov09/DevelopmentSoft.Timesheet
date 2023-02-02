using System;
using System.Collections.Generic;

namespace Timesheet.API.Services
{
    public class AuthService : IAuthService
    {
        public List<string> Employees { get; private set; }

        public AuthService()
        {
            Employees = new List<string>()
            {
                "Иванов",
                "Петров",
                "Сидоров"
            };
        }

        //Бизнес-логика + Domain
        public bool Login(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                return false;

            var isEmployeeExist = Employees.Contains(lastName);

            if (isEmployeeExist)
                UserSession.Session.Add(lastName);

            return isEmployeeExist;
        }
    }

    //Это слой Data(Persistence)
    public static class UserSession
    {
        static UserSession()
        {
            Session = new HashSet<string>();
        }

        public static HashSet<string> Session { get; set; }
    }
}
