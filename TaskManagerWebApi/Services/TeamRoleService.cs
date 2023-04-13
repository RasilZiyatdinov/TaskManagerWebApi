
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.DAL;
using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerWebApi.Services
{
    public class TeamRoleService : ITeamRoleService
    {
        private readonly ApplicationDbContext dbContext;

        public TeamRoleService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<IEnumerable<TeamRole>> GetTeamRolesAsync()
        {
            return await dbContext.TeamRole.ToListAsync();
        }


        public async Task<IEnumerable<string>> GetProjectTeamRolesAsync(int projectId)
        {
            var project = await dbContext.Project.Include(p => p.TeamRoles).FirstOrDefaultAsync(x => x.Id == projectId);
            return project != null ? project.TeamRoles.Select(r => r.Name) : null;
        }


        public async Task AddRolesToProjectAsync(int projectId, string roleName)
        {
            await dbContext.RoleProject.AddAsync(new RoleProject { Name = roleName, ProjectId = projectId });
            await dbContext.SaveChangesAsync();
        }


        public async Task DeleteRoleFromProjectAsync(int projectId, string roleName)
        {
            var project = await dbContext.Project.Include(p => p.TeamRoles).FirstOrDefaultAsync(x => x.Id == projectId);
            var roleProject = await dbContext.RoleProject.FirstOrDefaultAsync(x => x.ProjectId == projectId && x.Name == roleName);
            project.TeamRoles.Remove(roleProject);
            await dbContext.SaveChangesAsync();
        }


    }
}
