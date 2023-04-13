using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TaskManagerApi.DAL;
using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Models.DTO;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext dbContext;

        public TaskService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            var task = await dbContext.Task.FirstOrDefaultAsync(x => x.Id == taskId);
            var deleteResult = dbContext.Remove(task);
            await dbContext.SaveChangesAsync();
            return deleteResult != null ? true : false;
        }

        /// <summary>
        /// Добавить задачу
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddTaskAsync(TaskModel t)
        {
            var students = await dbContext.Users.Where(x => t.StudentIds.Contains(x.Id)).ToListAsync();
            TaskEntity task = new TaskEntity
            {
                Name = t.Name,
                CreationDate = DateTime.Now,
                ExpirationDate = t.ExpirationDate,
                Description = t.Description,
                PriorityId = t.PriorityId,
                StatusId = t.StatusId,
                ProjectId = t.ProjectId,
                TeamRoleId = t.TeamRoleId,
                Students = students
            };
            var result = await dbContext.Task.AddAsync(task);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskModel t)
        {
            var updatedTask = await dbContext.Task.FirstOrDefaultAsync(t => t.Id == t.Id);
            var students = await dbContext.Users.Where(x => t.StudentIds.Contains(x.Id)).ToListAsync();

            updatedTask.Name = t.Name;
            updatedTask.ExpirationDate = t.ExpirationDate;
            updatedTask.Description = t.Description;
            updatedTask.PriorityId = t.PriorityId;
            updatedTask.StatusId = t.StatusId;
            updatedTask.ProjectId = t.ProjectId;
            updatedTask.TeamRoleId = t.TeamRoleId;

            updatedTask.Students.Clear();
            updatedTask.Students.AddRange(students);

            var result = dbContext.Task.Update(updatedTask);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByStudentAsync(int studentId, int projectId)
        {
            var tasks = dbContext.Task.
                Where(x => x.ProjectId == projectId && x.Students.Select(x => x.Id).Contains(studentId)).
                    Select(t => new TaskDTO()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        ExpirationDate = t.ExpirationDate,
                        Description = t.Description,
                        Status = t.Status,
                        Priority = t.Priority,
                        ProjectId = t.ProjectId,
                        TeamRole = t.TeamRole.Name,
                 
                    });
            return tasks;
        }

        public async Task<IEnumerable<TaskDTO>> GetAllTasksAsync(int projectId)
        {
            var tasks = dbContext.Task.
                Where(x => x.ProjectId == projectId).
                    Select(t => new TaskDTO()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        ExpirationDate = t.ExpirationDate,
                        Description = t.Description,
                        Status = t.Status,
                        Priority = t.Priority,
                        ProjectId = t.ProjectId,
                        TeamRole = t.TeamRole.Name,
                        Students = t.Students.Select(s => new UserModel(s, null))
                    });
            return tasks;
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByRoleAsync(int roleId, int projectId)
        {
            var tasks = dbContext.Task.
                Where(x => x.ProjectId == projectId && x.TeamRoleId == roleId).
                    Select(t => new TaskDTO()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        ExpirationDate = t.ExpirationDate,
                        Description = t.Description,
                        Status = t.Status,
                        Priority = t.Priority,
                        ProjectId = t.ProjectId,
                        TeamRole = t.TeamRole.Name,
                        Students = t.Students.Select(s => new UserModel(s, null))
                    });
            return tasks;
        }
    }
}
