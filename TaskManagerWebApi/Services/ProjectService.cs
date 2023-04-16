using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TaskManagerWebApi.DAL;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Models.DTO;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerWebApi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext dbContext;
        private static ILogger<ProjectService>? logger;
        private readonly UserManager<User> userManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dbContext"></param>
        /// <param name="_userManager"></param>
        /// <param name="_logger"></param>
        public ProjectService(ApplicationDbContext _dbContext, UserManager<User> _userManager, ILogger<ProjectService> _logger)
        {
            dbContext = _dbContext;
            logger = _logger;
            userManager = _userManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public IEnumerable<ProjectDTO> GetProjectsByTeacherAsync(int teacherId)
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
                                Teacher = new UserDTO(p.Subject.Teacher, 
                                    dbContext.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).First())
                            },
                            Status = p.Status,
                            Manager = p.Manager != null ? new UserDTO(p.Manager, 
                                    dbContext.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).First()) : null,
                            Requests = p.Requests.Select(r => new RequestDTO() { Status = r.Status.Name, Student = new UserDTO(r.Student, 
                                dbContext.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).First()), Role = r.Role.Name }).ToList(),
                            TeamRoles = p.TeamRoles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).ToList()
                        });

            return projects;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProjectDTO>> GetProjectsByGroupAsync(int groupId)
        {
            var gr = await dbContext.Group.FirstAsync(x => x.Id == groupId);

            var projects = dbContext.Project.Include(p => p.Subject).ThenInclude(s => s.Teacher).
            Where(x => x.Subject.Groups.Contains(gr)).
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
                                   Teacher = new UserDTO(p.Subject.Teacher, dbContext.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).First())
                               },
                               Status = p.Status,
                               Manager = p.Manager != null ? new UserDTO(p.Manager, 
                                    dbContext.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).First()) : null,
                               Requests = p.Requests.Select(r => new RequestDTO() { Status = r.Status.Name, Student = new UserDTO(r.Student, 
                                    dbContext.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).First()), Role = r.Role.Name }).ToList(),
                               TeamRoles = p.TeamRoles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).ToList()
                           });

            return projects;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProjectDTO>> GetProjectsByGroupSubjAsync(int groupId, int subjectId)
        {
            var gr = await dbContext.Group.FirstAsync(x => x.Id == groupId);

            var projects = dbContext.Project.Include(p => p.Subject).ThenInclude(s => s.Teacher).ThenInclude(s => s.Group).
            Where(x => x.SubjectId == subjectId && x.Subject.Groups.Contains(gr)).
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
                                Teacher = new UserDTO(p.Subject.Teacher, 
                                    dbContext.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).First())
                            },
                            Status = p.Status,
                            Manager = p.Manager != null ? new UserDTO(p.Manager, 
                                dbContext.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).First()) : null,
                            Requests = p.Requests.Select(r => new RequestDTO() { Status = r.Status.Name, Student = new UserDTO(r.Student, 
                                dbContext.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).First()), Role = r.Role.Name }).ToList(),
                            TeamRoles = p.TeamRoles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).ToList()
                        });

            return projects;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserDTO>> GetProjectParticipants(int projectId)
        {
            var project = await dbContext.Project.
                Include(p => p.Requests).ThenInclude(p => p.Student).ThenInclude(s => s.Group).
                Include(p => p.Requests).ThenInclude(r => r.Role).FirstAsync(x => x.Id == projectId);

            var status = await dbContext.RequestStatus.FirstAsync(x => x.Name == "Принято");

            var users = project.Requests.Where(x => x.Status == status && x.Role.Name == "Студент").
                Select(x => new UserDTO(x.Student, 
                    dbContext.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).First())).ToList();

            return users;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task AddProjectAsync(ProjectModel p, UserDTO user)
        {
            var status = await dbContext.Status.FirstAsync(x => x.Name == "Открыта");

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
            logger?.LogInformation("Преподаватель добавил проект \'{Name}\': {@user}", p.Name, user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public async Task UpdateProjectAsync(ProjectModel p)
        {
            var pr = dbContext.Project.FirstAsync(x => x.Id == p.Id).Result;
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
        public async Task<bool> DeleteProjectAsync(int id, UserDTO user)
        {
            var pr = await dbContext.Project.FirstAsync(x => x.Id == id);
            var deleteResult = dbContext.Remove(pr);
            await dbContext.SaveChangesAsync();
            logger?.LogInformation("Преподаватель удалил проект \'{Name}\': {@user}", pr.Name, user);

            return deleteResult != null ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task LeaveProjectByManagerAsync(int projectId, UserDTO user)
        {
            var project = await dbContext.Project.FirstAsync(x => x.Id == projectId);
            if (project.Manager != null)
            {
                project.Manager = null;
                await dbContext.SaveChangesAsync();
                logger?.LogInformation("Менеджер покинул проект \'{Name}\': {@user}", project.Name, user);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="projectId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task LeaveProjectByStudentAsync(int studentId, int projectId, UserDTO user)
        {
            var project = await dbContext.Project.FirstAsync(x => x.Id == projectId);
            var student = await dbContext.Users.FirstAsync(x => x.Id == studentId);

            project.RequestedParticipants.Remove(student);
            await dbContext.SaveChangesAsync();
            logger?.LogInformation("Студент покинул проект \'{Name}\': {@user}", project.Name, user);
        }


    }
}
