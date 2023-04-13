using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManagerApi.DAL;
using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerWebApi.Entities;
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
        /// <param name="_logger"></param>
        public RequestService(ApplicationDbContext _dbContext, UserManager<User> _userManager, ILogger<ProjectService> _logger)
        {
            dbContext = _dbContext;
            logger = _logger;
            userManager = _userManager;
        }


        /// <summary>
        /// Подать заявку в проект
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="projectId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task RequestByStudentAsync(int studentId, int projectId, int roleId, UserModel user)
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
            project?.Requests?.Add(new Request { StudentId = studentId, Status = status, Role = role, Date = DateTime.Now });

            await dbContext.SaveChangesAsync();
            logger.LogInformation("Студент подал заявку на вступление в проект \'{Name}\': {@user}", project.Name, user);
        }

        /// <summary>
        /// Получить заявки в проект
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RequestDTO>> GetProjectRequestsAsync(int projectId, int roleId)
        {
            var project = await dbContext.Project.
                Include(p => p.Requests).ThenInclude(r => r.Role).
                Include(p => p.Requests).ThenInclude(r => r.Status).
                Include(p => p.Requests).ThenInclude(r => r.Student).ThenInclude(s => s.Group).
                FirstOrDefaultAsync(x => x.Id == projectId);
            return project.Requests.Where(r => r.Role.Id == roleId).Select(r => new RequestDTO() { Status = r.Status.Name, Student = new UserModel(r.Student, null), Role = r.Role.Name });
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
            var project = await dbContext.Project.Include(x => x.Requests).FirstOrDefaultAsync(x => x.Id == projectId);
            var status = await dbContext.RequestStatus.FirstOrDefaultAsync(x => x.Name == "Принято");

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
                    logger.LogInformation("Заявка студента в проект принята \'{Name}\': {@user}", project.Name, req.Student);
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
            var project = await dbContext.Project.Include(x => x.Requests).FirstOrDefaultAsync(x => x.Id == projectId);
            var status = await dbContext.RequestStatus.FirstOrDefaultAsync(x => x.Name == "Принято");

            var accepted = project.Requests.Where(x => x.Status == status).ToList();

            if (project.Manager != null)
            {
                throw new AppException("Проект уже занят менеджером");
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
                    project.ManagerId = studentId;
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("Заявка менеджера в проект принята \'{Name}\': {@user}", project.Name, req.Student);
                }
            }
        }

        public async Task LeaveProjectByManagerAsync(int projectId, UserModel user)
        {
            var project = await dbContext.Project.FirstOrDefaultAsync(x => x.Id == projectId);
            if (project.Manager != null)
            {
                project.Manager = null;
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Менеджер покинул проект \'{Name}\': {@user}", project.Name, user);
            }
        }

        public async Task LeaveProjectByStudentAsync(int studentId, int projectId, UserModel user)
        {
            var project = await dbContext.Project.FirstOrDefaultAsync(x => x.Id == projectId);
            var student = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == studentId);

            project.RequestedParticipants.Remove(student);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Студент покинул проект \'{Name}\': {@user}", project.Name, user);
        }





        /// <summary>
        /// Отклонить заявку в проект (менеджер)
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public async Task RejectProjectRequestsAsync(int projectId, int studentId)
        {
            var project = await dbContext.Project.FirstOrDefaultAsync(x => x.Id == projectId);
            var status = await dbContext.RequestStatus.FirstOrDefaultAsync(x => x.Name == "Отклонено");
            var req = project.Requests.Where(x => x.StudentId == studentId && x.ProjectId == projectId).First();
            if (req.Status == status)
            {
                throw new AppException("Заявка уже отклонена");
            }
            else
            {
                req.Status = status;
                logger.LogInformation("Заявка в проект отклонена \'{Name}\': {@user}", project.Name, req.Student);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
