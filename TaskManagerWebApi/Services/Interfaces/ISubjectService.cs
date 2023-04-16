using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Models.DTO;

namespace TaskManagerWebApi.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISubjectService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<SubjectDTO>> GetSubjectByTeacherAsync(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        Task AddSubjectAsync(SubjectModel subject);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteSubjectAsync(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        Task UpdateSubjectAsync(SubjectModel subject);
    }
}
