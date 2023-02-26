using TaskManagerApi.Entities;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectModel>> GetProjectsByTeacherAsync(int id);
        Task<Project> AddProjectAsync(Project g, UserModelResponse user);
        Task<Project> UpdateProjectAsync(Project g);
        Task<bool> DeleteProjectAsync(int id, UserModelResponse user);
        Task<Project> ChooseProjectByManagerAsync(int managerId, int projectId, UserModelResponse user);
        Task<Project> ChooseProjectByStudentAsync(int studentId, int projectId, UserModelResponse user);
        Task<Project> LeaveProjectByManagerAsync(int projectId, UserModelResponse user);
        Task<Project> LeaveProjectByStudentAsync(int studentId, int projectId, UserModelResponse user);
    }
}
