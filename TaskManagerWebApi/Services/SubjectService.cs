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
    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDbContext dbContext;
        private static ILogger<SubjectService>? logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dbContext"></param>
        /// <param name="_logger"></param>
        public SubjectService(ApplicationDbContext _dbContext, ILogger<SubjectService> _logger)
        {
            dbContext = _dbContext;
            logger = _logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        public async Task AddSubjectAsync(SubjectModel subject)
        {
            var groups = await dbContext.Group.Where(x => subject.GroupsIds.Contains(x.Id)).ToListAsync();
            var teacher = await dbContext.Users.FirstAsync(x => x.Id == subject.TeacherId);
            var subj = await dbContext.AddAsync(new Subject
            {
                Name = subject.Name,
                Groups = (List<Group>)groups,
                Teacher = teacher,
            });
            await dbContext.SaveChangesAsync();
            //logger.LogInformation("Преподаватель добавил новую дисциплину \'{Name}\': {@user}", subject.Name, user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteSubjectAsync(int id)
        {
            var subject = await dbContext.Subject.FirstAsync(x => x.Id == id);
            var deleteResult = dbContext.Remove(subject);
            await dbContext.SaveChangesAsync();
            //logger.LogInformation("Преподаватель удалил дисциплину \'{Name}\': {@user}", subject.Name, user);
            return deleteResult != null ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subjectmodel"></param>
        /// <returns></returns>
        public async Task UpdateSubjectAsync(SubjectModel subjectmodel)
        {
            var subject = await dbContext.Subject.Include(x => x.Groups).FirstAsync(x => x.Id == subjectmodel.Id);
            var groups = await dbContext.Group.Where(x => subjectmodel.GroupsIds.Contains(x.Id)).ToListAsync();
            subject.Name = subjectmodel.Name;
            subject.Groups.Clear();
            subject.Groups.AddRange(groups);
            var updateResult = dbContext.Subject.Update(subject);
            await dbContext.SaveChangesAsync();
            //logger.LogInformation("Преподаватель обновил дисциплину \'{Name}\': {@user}", subject.Name, user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SubjectDTO>> GetSubjectByTeacherAsync(int id)
        {
            var subjectList = await dbContext.Subject.Include(x => x.Groups).Include(x => x.Teacher).Where(x => x.TeacherId == id).ToListAsync();
            List<SubjectDTO> list = new List<SubjectDTO>();
            foreach (var item in subjectList)
            {
                list.Add(new SubjectDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Teacher = new UserDTO(item.Teacher, 
                        dbContext.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name }).First()),
                    Groups = item.Groups,
                });
            }
            return list;
        }
    }
}
