using TaskManagerApi.Entities;
using TaskManagerApi.Models;
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
        Task<IEnumerable<ProjectDTO>> GetProjectsByTeacherAsync(int id);

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
        Task AddProjectAsync(ProjectModel g, UserModel user);

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
        Task<bool> DeleteProjectAsync(int id, UserModel user);

    }
}
