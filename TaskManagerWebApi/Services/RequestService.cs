using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class RequestService : IRequestService
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
        public RequestService(ApplicationDbContext _dbContext, UserManager<User> _userManager, ILogger<ProjectService> _logger)
        {
            dbContext = _dbContext;
            logger = _logger;
            userManager = _userManager;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="projectId"></param>
        /// <param name="roleId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task PostRequestAsync(int studentId, int projectId, int roleId, UserDTO user)
        {
            var project = await dbContext.Project.FirstOrDefaultAsync(x => x.Id == projectId);
            var status = await dbContext.RequestStatus.FirstAsync(x => x.Name == "Запрошено");
            var role = await dbContext.Roles.FirstAsync(x => x.Id == roleId);

            var student = await dbContext.Users.Include(x => x.Requests).
                ThenInclude(x => x.Project).
                ThenInclude(x => x.Subject).FirstOrDefaultAsync(x => x.Id == studentId);

            //var isOneSubj = student.Requests.Where(r => r.Project.Subject.Equals(project.Subject));
            //if (isOneSubj.Count() != 0)
            //{
            //    throw new AppException("Заявка в проект по данной дисциплине уже подана");
            //}
            project?.Requests?.Add(new Request { StudentId = studentId, Status = status, Role = role, Date = DateTime.UtcNow });

            await dbContext.SaveChangesAsync();
            logger?.LogInformation("Студент подал заявку на вступление в проект \'{Name}\': {@user}", project?.Name, user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RequestDTO>> GetProjectRequestsAsync(int projectId, int roleId)
        {
            var project = await dbContext.Project.
                Include(p => p.Requests).ThenInclude(r => r.Role).
                Include(p => p.Requests).ThenInclude(r => r.Status).
                Include(p => p.Requests).ThenInclude(r => r.Student).ThenInclude(s => s.Group).
                FirstAsync(x => x.Id == projectId);
            return project.Requests.Where(r => r.Role.Id == roleId).Select(r => new RequestDTO() { Date = r.Date, Status = r.Status.Name, Student = new UserDTO(r.Student, 
                    dbContext.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).First()), Role = r.Role.Name });
        }

        /// <summary>
        /// Принять заявку студента в проект (менеджер)
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task AcceptProjectRequestStudentAsync(int projectId, int studentId)
        {
            var project = await dbContext.Project.Include(x => x.Requests).FirstAsync(x => x.Id == projectId);
            var status = await dbContext.RequestStatus.FirstAsync(x => x.Name == "Принято");

            var accepted = project.Requests.Where(x => x.Status == status).ToList();

            if (accepted.Count == project.MembersNum)
            {
                throw new AppException("Количество участников максимально");
            }
            else
            {
                var req = project.Requests.Where(x => x.StudentId == studentId && x.ProjectId == projectId).First();
                if (req.Status == status)
                {
                    throw new AppException("Заявка уже принята");
                }
                else
                {
                    req.Status = status;
                    await dbContext.SaveChangesAsync();
                    logger?.LogInformation("Заявка студента в проект принята \'{Name}\': {@user}", project.Name, req.Student);
                }
            }
        }

        /// <summary>
        /// Принять завку менеджера в проект (преподаватель)
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task AcceptProjectRequestManagerAsync(int projectId, int studentId)
        {
            var project = await dbContext.Project.Include(x => x.Requests).FirstAsync(x => x.Id == projectId);
            var status = await dbContext.RequestStatus.FirstAsync(x => x.Name == "Принято");

            var accepted = project.Requests.Where(x => x.Status == status).ToList();

            if (project.Manager != null)
            {
                throw new AppException("Проект уже занят менеджером");
            }
            else
            {
                var request = project.Requests.Where(x => x.StudentId == studentId && x.ProjectId == projectId).First();
                if (request.Status == status)
                {
                    throw new AppException("Заявка уже принята");
                }
                else
                {
                    request.Status = status;
                    project.ManagerId = studentId;
                    await dbContext.SaveChangesAsync();
                    logger?.LogInformation("Заявка менеджера в проект принята \'{Name}\': {@user}", project.Name, request.Student);
                }
            }
        }
   
        /// <summary>
        /// Отклонить заявку в проект (менеджер)
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task RejectProjectRequestsAsync(int projectId, int studentId)
        {
            var project = await dbContext.Project.FirstAsync(x => x.Id == projectId);
            var status = await dbContext.RequestStatus.FirstAsync(x => x.Name == "Отклонено");
            var req = project.Requests.Where(x => x.StudentId == studentId && x.ProjectId == projectId).First();
            if (req.Status == status)
            {
                throw new AppException("Заявка уже отклонена");
            }
            else
            {
                req.Status = status;
                logger?.LogInformation("Заявка в проект отклонена \'{Name}\': {@user}", project.Name, req.Student);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
