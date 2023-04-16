using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models.DTO;

namespace TaskManagerWebApi.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITeamRoleService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TeamRole>> GetTeamRolesAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task AddRolesToProjectAsync(int projectId, string roleName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task DeleteRoleFromProjectAsync(int projectId, string roleName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<IEnumerable<RoleDTO>> GetProjectTeamRolesAsync(int projectId);
    }
}
