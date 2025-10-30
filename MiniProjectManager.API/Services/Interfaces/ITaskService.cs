using MiniProjectManager.API.Models.DTOs.Tasks;

namespace MiniProjectManager.API.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponseDto>> GetTasksByProjectIdAsync(int projectId, int userId);
        Task<TaskResponseDto?> GetTaskByIdAsync(int taskId, int userId);
        Task<TaskResponseDto?> CreateTaskAsync(int projectId, CreateTaskDto createTaskDto, int userId);
        Task<TaskResponseDto?> UpdateTaskAsync(int taskId, UpdateTaskDto updateTaskDto, int userId);
        Task<bool> DeleteTaskAsync(int taskId, int userId);
        Task<TaskResponseDto?> ToggleTaskCompletionAsync(int taskId, int userId);
    }
}