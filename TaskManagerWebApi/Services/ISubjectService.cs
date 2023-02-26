using TaskManagerApi.Entities;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectModel>> GetSubjectByTeacherAsync(int id);
        Task<Subject> AddSubjectAsync(SubjectModel subject);
        Task<bool> DeleteSubjectAsync(int id);
        Task<Subject> UpdateSubjectAsync(SubjectModel subject);
        Task<Subject> GetSubjectByIdAsync(int id);

    }
}
