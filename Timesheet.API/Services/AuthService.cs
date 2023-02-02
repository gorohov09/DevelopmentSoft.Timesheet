using System;
using System.Collections.Generic;

namespace Timesheet.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserSession _userSession;

        public AuthService(UserSession userSession)
        {
            _userSession = userSession;
            Employees = new List<string>()
            {
                "Иванов",
                "Петров",
                "Сидоров"
            };
        }

        public List<string> Employees { get; private set; }


        //Бизнес-логика + Domain
        public bool Login(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                return false;

            var isEmployeeExist = Employees.Contains(lastName);

            if (isEmployeeExist)
                _userSession.Session.Add(lastName);

            return isEmployeeExist;
        }
    }

    //Это слой Data(Persistence)
    public class UserSession
    {
        public UserSession()
        {
            Session = new HashSet<string>();
        }

        public HashSet<string> Session { get; set; }
    }
}
