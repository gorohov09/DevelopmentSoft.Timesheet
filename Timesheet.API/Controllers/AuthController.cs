using Microsoft.AspNetCore.Mvc;
using Timesheet.API.Models;
using Timesheet.API.Services;

namespace Timesheet.API.Controllers
{
    //Presentet
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public ActionResult<bool> Test([FromBody]LoginRequest request)
        {
            var authService = new AuthService();

            return Ok(authService.Login(request.LastName));
        }
    }
}
