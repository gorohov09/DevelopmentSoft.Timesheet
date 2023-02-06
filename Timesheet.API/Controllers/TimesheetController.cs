using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Timesheet.API.ResourceModels;
using Timesheet.Domain.Interfaces;
using Timesheet.Domain.Models;

namespace Timesheet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : ControllerBase
    {
        private readonly ITimesheetService _timesheetService;
        private readonly ITimesheetRepository _timesheetRepository;

        public TimesheetController(ITimesheetService timesheetService, ITimesheetRepository timesheetRepository)
        {
            _timesheetService = timesheetService;
            _timesheetRepository = timesheetRepository;
        }

        [HttpPost("log")]
        public ActionResult LogTime([FromBody] TimeLogRequest request)
        {
            return Ok(_timesheetService.TrackTime(new TimeLog
            {
                LastName = request.LastName,
                Date = request.Date,
                Comment = request.Comment,
                WorkingHours = request.WorkingHours,
            }));
        }

        [HttpGet("getlogs")]
        public IActionResult GetLogTime()
        {
            var a = _timesheetRepository.GetTimeLogs("Иванов");
            return Ok(a);
        }

    }
}
