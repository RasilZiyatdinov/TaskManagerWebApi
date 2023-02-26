using Microsoft.EntityFrameworkCore;
using TaskManagerApi.DAL;
using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerApi.Services.Interfaces;

namespace TaskManagerApi.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext dbContext;
        private static ILogger<ProjectService> logger;

        public ProjectService(ApplicationDbContext _dbContext, ILogger<ProjectService> _logger)
        {
            dbContext = _dbContext;
            logger = _logger;

        }
        public async Task<IEnumerable<ProjectModel>> GetProjectsByTeacherAsync(int teacherId)
        {
            var projectList = await dbContext.Project.Include(p => p.Manager).
                Include(p => p.Participants).
                Include(p => p.Subject).ThenInclude(s => s.Teacher).
                Where(x => x.Subject.TeacherId == teacherId).ToListAsync();
            IEnumerable<ProjectModel> result = new List<ProjectModel>();
            List<string> parts = new List<string>();
            foreach (var item in projectList)
            {
                result.Append(new ProjectModel {
                    Id = item.Id,
                    Name = item.Name, 
                    MembersNum = item.MembersNum, 
                    Subject = item.Subject.Id,
                    Teacher = item.Subject.Teacher.Id,
                    //Manager = item.Manager.Id,
                    Participants = item.Participants.Select(x => x.Id).ToList()
                });
            }
            return result;
        }

        public async Task<IEnumerable<ProjectModel>> GetProjectsByGroupAsync(int groupId)
        {
            var group = await dbContext.Group.FirstOrDefaultAsync(x => x.Id == groupId);
            var projectList = await dbContext.Project.Where(x => x.Subject.Groups.Contains(group)).ToListAsync();
            IEnumerable<ProjectModel> result = new List<ProjectModel>();
            foreach (var item in projectList)
            {
                result.Append(new ProjectModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    MembersNum = item.MembersNum,
                    Subject = item.Subject.Id,
                    Teacher = item.Subject.Teacher.Id,
                    Participants = item.Participants.Select(x => x.Id).ToList()
                });
            }
            return result;
        }


        public async Task<Project> AddProjectAsync(Project p, UserModelResponse user)
        {
            var pr = await dbContext.Project.AddAsync(p);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Преподаватель добавил проект \'{Name}\': {@user}", p.Name, user);
            return pr.Entity;
        }

        public async Task<Project> UpdateProjectAsync(Project p)
        {
            var updateResult = dbContext.Project.Update(p);
            await dbContext.SaveChangesAsync();

            return updateResult.Entity;
        }

        public async Task<bool> DeleteProjectAsync(int id, UserModelResponse user)
        {
            var pr = await dbContext.Project.FirstOrDefaultAsync(x => x.Id == id);
            var deleteResult = dbContext.Remove(pr);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Преподаватель удалмл проект \'{Name}\': {@user}", pr.Name, user);

            return deleteResult != null ? true : false;
        }


        public async Task<Project> ChooseProjectByManagerAsync(int managerId, int projectId, UserModelResponse user)
        {
            var project = await dbContext.Project.FirstOrDefaultAsync(x => x.Id == projectId);
            if (project.Manager == null)
            {
                var manager = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == managerId);
                project.Manager = manager;
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Менеджер выбрал проект \'{Name}\': {@user}", project.Name, user);
            }

            return project;
        }

        public async Task<Project> ChooseProjectByStudentAsync(int studentId, int projectId, UserModelResponse user)
        {
            var project = await dbContext.Project.FirstOrDefaultAsync(x => x.Id == projectId);
            if (project.Participants.Count < project.MembersNum)
            {
                var student = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == studentId);
                project.Participants.Add(student);
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Студент записался в проект \'{Name}\': {@user}", project.Name, user);
            }

            return project;
        }


        public async Task<Project> LeaveProjectByManagerAsync(int projectId, UserModelResponse user)
        {
            var project = await dbContext.Project.FirstOrDefaultAsync(x => x.Id == projectId);
            if (project.Manager != null)
            {
                project.Manager = null;
                await dbContext.SaveChangesAsync();
                logger.LogInformation("Менеджер покинул проект \'{Name}\': {@user}", project.Name, user);
            }

            return project;
        }

        public async Task<Project> LeaveProjectByStudentAsync(int studentId, int projectId, UserModelResponse user)
        {
            var project = await dbContext.Project.FirstOrDefaultAsync(x => x.Id == projectId);
            var student = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == studentId);

            project.Participants.Remove(student);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Студент покинул проект \'{Name}\': {@user}", project.Name, user);

            return project;
        }



    }
}
