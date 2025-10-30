using MiniProjectManager.API.Models.DTOs.Scheduler;

namespace MiniProjectManager.API.Services.Interfaces
{
    public interface ISchedulerService
    {
        Task<ScheduleResponseDto?> GenerateScheduleAsync(int projectId, ScheduleRequestDto request, int userId);
    }
}