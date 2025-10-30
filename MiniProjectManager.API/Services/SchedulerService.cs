using Microsoft.EntityFrameworkCore;
using MiniProjectManager.API.Data;
using MiniProjectManager.API.Helpers;
using MiniProjectManager.API.Models.DTOs.Scheduler;
using MiniProjectManager.API.Services.Interfaces;

namespace MiniProjectManager.API.Services
{
    public class SchedulerService : ISchedulerService
    {
        private readonly AppDbContext _context;

        public SchedulerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ScheduleResponseDto?> GenerateScheduleAsync(int projectId, ScheduleRequestDto request, int userId)
        {
            // Verify project belongs to user
            var project = await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == projectId && p.UserId == userId);

            if (project == null)
            {
                return null;
            }

            var tasks = project.Tasks.ToList();
            var scheduledTasks = SchedulerAlgorithm.ScheduleTasks(
                tasks, 
                request.StartDate, 
                request.AvailableHoursPerDay
            );

            var response = new ScheduleResponseDto
            {
                StartDate = request.StartDate,
                TotalTasks = tasks.Count,
                ScheduledTasksCount = scheduledTasks.Count,
                EstimatedCompletionDate = scheduledTasks.Any() 
                    ? scheduledTasks.Max(st => st.ScheduledDate) 
                    : request.StartDate,
                ScheduledTasks = scheduledTasks.Select(st => new ScheduledTaskDto
                {
                    TaskId = st.Task.Id,
                    Title = st.Task.Title,
                    ScheduledDate = st.ScheduledDate,
                    OriginalDueDate = st.Task.DueDate,
                    IsOverdue = st.Task.DueDate.HasValue && st.Task.DueDate.Value < DateTime.UtcNow,
                    Priority = st.Priority
                }).ToList()
            };

            return response;
        }
    }
}