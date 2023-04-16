
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.DAL;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models.DTO;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerWebApi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class TeamRoleService : ITeamRoleService
    {
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dbContext"></param>
        public TeamRoleService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TeamRole>> GetTeamRolesAsync()
        {
            return await dbContext.TeamRole.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RoleDTO>> GetProjectTeamRolesAsync(int projectId)
        {
            var project = await dbContext.Project.Include(p => p.TeamRoles).FirstAsync(x => x.Id == projectId);
            return project.TeamRoles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task AddRolesToProjectAsync(int projectId, string roleName)
        {
            await dbContext.RoleProject.AddAsync(new RoleProject { Name = roleName, ProjectId = projectId });
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task DeleteRoleFromProjectAsync(int projectId, string roleName)
        {
            var project = await dbContext.Project.Include(p => p.TeamRoles).FirstAsync(x => x.Id == projectId);
            var roleProject = await dbContext.RoleProject.FirstAsync(x => x.ProjectId == projectId && x.Name == roleName);
            project.TeamRoles.Remove(roleProject);
            await dbContext.SaveChangesAsync();
        }


    }
}
