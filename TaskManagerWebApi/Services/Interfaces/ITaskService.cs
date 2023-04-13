using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Models.DTO;

namespace TaskManagerWebApi.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDTO>> GetTasksByStudentAsync(int studentId, int projectId);
        //Task<TaskEntity> AddTaskAsync(SubjectModel subject);
        Task<bool> DeleteTaskAsync(int taskId);

        Task AddTaskAsync(TaskModel t);
        Task UpdateTaskAsync(TaskModel t);

        Task<IEnumerable<TaskDTO>> GetAllTasksAsync(int projectId);
        Task<IEnumerable<TaskDTO>> GetTasksByRoleAsync(int roleId, int projectId);

        //Task<Subject> UpdateTaskAsync(SubjectModel subject);
    }
}
