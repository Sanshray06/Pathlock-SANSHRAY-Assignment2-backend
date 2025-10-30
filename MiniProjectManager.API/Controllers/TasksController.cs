using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniProjectManager.API.Models.DTOs.Tasks;
using MiniProjectManager.API.Services.Interfaces;

namespace MiniProjectManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/projects/{projectId}/tasks")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdClaim ?? "0");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetTasksByProjectId(int projectId)
        {
            var userId = GetUserId();
            var tasks = await _taskService.GetTasksByProjectIdAsync(projectId, userId);
            return Ok(tasks);
        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<TaskResponseDto>> GetTaskById(int taskId)
        {
            var userId = GetUserId();
            var task = await _taskService.GetTaskByIdAsync(taskId, userId);

            if (task == null)
            {
                return NotFound(new { message = "Task not found" });
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskResponseDto>> CreateTask(int projectId, [FromBody] CreateTaskDto createTaskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetUserId();
            var task = await _taskService.CreateTaskAsync(projectId, createTaskDto, userId);

            if (task == null)
            {
                return NotFound(new { message = "Project not found" });
            }

            return CreatedAtAction(nameof(GetTaskById), new { projectId, taskId = task.Id }, task);
        }

        [HttpPut("{taskId}")]
        public async Task<ActionResult<TaskResponseDto>> UpdateTask(int taskId, [FromBody] UpdateTaskDto updateTaskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = GetUserId();
            var task = await _taskService.UpdateTaskAsync(taskId, updateTaskDto, userId);

            if (task == null)
            {
                return NotFound(new { message = "Task not found" });
            }

            return Ok(task);
        }

        [HttpPatch("{taskId}/toggle")]
        public async Task<ActionResult<TaskResponseDto>> ToggleTaskCompletion(int taskId)
        {
            var userId = GetUserId();
            var task = await _taskService.ToggleTaskCompletionAsync(taskId, userId);

            if (task == null)
            {
                return NotFound(new { message = "Task not found" });
            }

            return Ok(task);
        }

        [HttpDelete("{taskId}")]
        public async Task<ActionResult> DeleteTask(int taskId)
        {
            var userId = GetUserId();
            var result = await _taskService.DeleteTaskAsync(taskId, userId);

            if (!result)
            {
                return NotFound(new { message = "Task not found" });
            }

            return NoContent();
        }
    }
}