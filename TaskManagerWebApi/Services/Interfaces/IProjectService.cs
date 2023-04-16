using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Models.DTO;

namespace TaskManagerWebApi.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<ProjectDTO> GetProjectsByTeacherAsync(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task<IEnumerable<ProjectDTO>> GetProjectsByGroupAsync(int groupId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddProjectAsync(ProjectModel g, UserDTO user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        Task UpdateProjectAsync(ProjectModel g);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> DeleteProjectAsync(int id, UserDTO user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task LeaveProjectByManagerAsync(int projectId, UserDTO user);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="projectId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task LeaveProjectByStudentAsync(int studentId, int projectId, UserDTO user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        Task<IEnumerable<ProjectDTO>> GetProjectsByGroupSubjAsync(int groupId, int subjectId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<IEnumerable<UserDTO>> GetProjectParticipants(int projectId);
    }
}
