using MiniProjectManager.API.Models.DTOs.Projects;

namespace MiniProjectManager.API.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponseDto>> GetAllProjectsAsync(int userId);
        Task<ProjectResponseDto?> GetProjectByIdAsync(int projectId, int userId);
        Task<ProjectResponseDto?> CreateProjectAsync(CreateProjectDto createProjectDto, int userId);
        Task<ProjectResponseDto?> UpdateProjectAsync(int projectId, UpdateProjectDto updateProjectDto, int userId);
        Task<bool> DeleteProjectAsync(int projectId, int userId);
    }
}