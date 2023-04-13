using TaskManagerApi.Entities;

namespace TaskManagerWebApi.Services.Interfaces
{
    public interface ITeamRoleService
    {
        Task<IEnumerable<TeamRole>> GetTeamRolesAsync();
        Task AddRolesToProjectAsync(int projectId, string roleName);
        Task DeleteRoleFromProjectAsync(int projectId, string roleName);
        Task<IEnumerable<string>> GetProjectTeamRolesAsync(int projectId);
    }
}
