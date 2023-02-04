using Microsoft.AspNetCore.Mvc;
using Timesheet.API.ResourceModels;
using Timesheet.Domain.Interfaces;

namespace Timesheet.API.Controllers
{
    //Presentet
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public ActionResult<bool> Test([FromBody]LoginRequest request)
        {
            return Ok(_authService.Login(request.LastName));
        }
    }
}
