using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerWebApi.Models.DTO;

namespace TaskManagerWebApi.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDTO>> GetSubjectByTeacherAsync(int id);
        Task AddSubjectAsync(SubjectModel subject);
        Task<bool> DeleteSubjectAsync(int id);
        Task UpdateSubjectAsync(SubjectModel subject);
    }
}
