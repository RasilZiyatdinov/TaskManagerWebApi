using TaskManagerWebApi.Models;
using TaskManagerWebApi.Models.DTO;

namespace TaskManagerWebApi.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        IEnumerable<TaskDTO> GetTasksByStudentAsync(int studentId, int projectId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        Task<bool> DeleteTaskAsync(int taskId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task AddTaskAsync(TaskModel t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task UpdateTaskAsync(TaskModel t);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        IEnumerable<TaskDTO> GetAllTasksAsync(int projectId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        IEnumerable<TaskDTO> GetTasksByRoleAsync(int roleId, int projectId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task UpdateTaskByStudentAsync(TaskStudentModel t);
    }
}
