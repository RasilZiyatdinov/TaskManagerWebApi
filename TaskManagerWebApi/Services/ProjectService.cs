using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System.Linq;
using System.Xml.Linq;
using TaskManagerApi.DAL;
using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Models.DTO;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerApi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext dbContext;
        private static ILogger<ProjectService> logger;
        private readonly UserManager<User> userManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dbContext"></param>
        /// <param name="_logger"></param>
        public ProjectService(ApplicationDbContext _dbContext, UserManager<User> _userManager, ILogger<ProjectService> _logger)
        {
            dbContext = _dbContext;
            logger = _logger;
            userManager = _userManager;
        }


        public async Task<IEnumerable<ProjectDTO>> GetProjectsByTeacherAsync(int teacherId)
        {
            var projects = dbContext.Project.Include(p => p.Subject).ThenInclude(s => s.Teacher).ThenInclude(s => s.Group).
            Where(x => x.Subject.TeacherId == teacherId).
                        Select(p => new ProjectDTO()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            MembersNum = p.MembersNum,
                            ExpirationDate = p.ExpirationDate,
                            Subject = new SubjectDTO
                            {
                                Id = p.SubjectId,
                                Name = p.Subject.Name,
                                Teacher = new UserModel(p.Subject.Teacher, null)
                            },
                            Status = p.Status.Name,
                            Manager = p.Manager != null ? new UserModel(p.Manager, null) : null,
                            Requests = p.Requests.Select(r => new RequestDTO() { Status = r.Status.Name, Student = new UserModel(r.Student, null), Role = r.Role.Name }),
                            TeamRoles = p.TeamRoles.Select(r => r.Name).ToList()
                        });

            return projects;
        }

        public async Task<IEnumerable<ProjectDTO>> GetProjectsByGroupAsync(int groupId)
        {
            var gr = await dbContext.Group.FirstOrDefaultAsync(x => x.Id == groupId);

            var projects = from p in dbContext.Project.Include(p => p.Subject).ThenInclude(s => s.Teacher).
            Where(x => x.Subject.Groups.Contains(gr))
                           select new ProjectDTO()
                           {
                               Id = p.Id,
                               Name = p.Name,
                               MembersNum = p.MembersNum,
                               ExpirationDate = p.ExpirationDate,
                               Subject = new SubjectDTO
                               {
                                   Id = p.SubjectId,
                                   Name = p.Subject.Name,
                                   Teacher = new UserModel(p.Subject.Teacher, null)
                               },
                               Status = p.Status.Name,
                               Manager = p.Manager != null ? new UserModel(p.Manager, null) : null,
                               Requests = p.Requests.Select(r => new RequestDTO() { Status = r.Status.Name, Student = new UserModel(r.Student, null), Role = r.Role.Name }),
                               TeamRoles = p.TeamRoles.Select(r => r.Name).ToList()
                           };

            return projects;
        }

        public async Task AddProjectAsync(ProjectModel p, UserModel user)
        {
            var status = await dbContext.Status.FirstOrDefaultAsync(x => x.Name == "Открыта");

            Project pr = new Project
            {
                Name = p.Name,
                MembersNum = p.MembersNum,
                SubjectId = p.SubjectId,
                ExpirationDate = p.PlannedExpirationDate,
                Status = status                
            };
            var project = await dbContext.Project.AddAsync(pr);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Преподаватель добавил проект \'{Name}\': {@user}", p.Name, user);
        }

        public async Task UpdateProjectAsync(ProjectModel p)
        {
            var pr = dbContext.Project.FirstOrDefaultAsync(x => x.Id == p.Id).Result;
            pr.Name = p.Name;
            pr.MembersNum = p.MembersNum;
            pr.SubjectId = p.SubjectId;

            var updateResult = dbContext.Project.Update(pr);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Удаление проекта
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProjectAsync(int id, UserModel user)
        {
            var pr = await dbContext.Project.FirstOrDefaultAsync(x => x.Id == id);
            var deleteResult = dbContext.Remove(pr);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Преподаватель удалил проект \'{Name}\': {@user}", pr.Name, user);

            return deleteResult != null ? true : false;
        }

    }
}
