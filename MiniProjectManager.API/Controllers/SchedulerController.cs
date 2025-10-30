using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniProjectManager.API.Models.DTOs.Scheduler;
using MiniProjectManager.API.Services.Interfaces;

namespace MiniProjectManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/projects/{projectId}/schedule")]
    [Authorize]
    public class SchedulerController : ControllerBase
    {
        private readonly ISchedulerService _schedulerService;

        public SchedulerController(ISchedulerService schedulerService)
        {
            _schedulerService = schedulerService;
        }

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdClaim ?? "0");
        }

        [HttpPost]
        public async Task<ActionResult<ScheduleResponseDto>> GenerateSchedule(
            int projectId, 
            [FromBody] ScheduleRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetUserId();
            var schedule = await _schedulerService.GenerateScheduleAsync(projectId, request, userId);

            if (schedule == null)
            {
                return NotFound(new { message = "Project not found" });
            }

            return Ok(schedule);
        }
    }
}