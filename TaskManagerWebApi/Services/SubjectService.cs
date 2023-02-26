using Microsoft.EntityFrameworkCore;
using TaskManagerApi.DAL;
using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerApi.Services.Interfaces;

namespace TaskManagerWebApi.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDbContext dbContext;
        private static ILogger<SubjectService> logger;
        public SubjectService(ApplicationDbContext _dbContext, ILogger<SubjectService> _logger)
        {
            dbContext = _dbContext;
            logger = _logger;
        }

        public async Task<Subject> AddSubjectAsync(SubjectModel subject)
        {
            var groups = await dbContext.Group.Where(x => subject.GroupsIds.Contains(x.Id)).ToListAsync();
            var teacher = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == subject.TeacherId);
            var subj = await dbContext.AddAsync(new Subject
            {
                Name = subject.Name,
                Groups = (List<Group>)groups,
                Teacher = teacher,
            });
            await dbContext.SaveChangesAsync();
            //logger.LogInformation("Преподаватель добавил новую дисциплину \'{Name}\': {@user}", subject.Name, user);
            return subj.Entity;

        }

        public async Task<bool> DeleteSubjectAsync(int id)
        {
            var subject = await dbContext.Subject.FirstOrDefaultAsync(x => x.Id == id);
            var deleteResult = dbContext.Remove(subject);
            await dbContext.SaveChangesAsync();
            //logger.LogInformation("Преподаватель удалил дисциплину \'{Name}\': {@user}", subject.Name, user);
            return deleteResult != null ? true : false;
        }

        public async Task<Subject> UpdateSubjectAsync(SubjectModel subjectmodel)
        {
            var subject = await dbContext.Subject.Include(x => x.Groups).FirstOrDefaultAsync(x => x.Id == subjectmodel.Id);
            var groups = await dbContext.Group.Where(x => subjectmodel.GroupsIds.Contains(x.Id)).ToListAsync();
            subject.Name = subjectmodel.Name;
            subject.Groups.Clear();
            subject.Groups.AddRange(groups);
            var updateResult = dbContext.Subject.Update(subject);
            await dbContext.SaveChangesAsync();
            //logger.LogInformation("Преподаватель обновил дисциплину \'{Name}\': {@user}", subject.Name, user);
            return updateResult.Entity;
        }

        public async Task<IEnumerable<SubjectModel>> GetSubjectByTeacherAsync(int id)
        {
            var subjectList = await dbContext.Subject.Include(x => x.Groups).Where(x => x.TeacherId == id).ToListAsync();
            List<SubjectModel> list = new List<SubjectModel>();
            foreach (var item in subjectList)
            {
                list.Add(new SubjectModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    TeacherId = item.TeacherId,
                    GroupsIds = item.Groups.Select(x => x.Id).ToList(),
                });
            }
            return list;
        }

        public async Task<Subject> GetSubjectByIdAsync(int id)
        {
            var subject = await dbContext.Subject.FirstOrDefaultAsync(x => x.Id == id);
            return subject;
        }
    }
}
