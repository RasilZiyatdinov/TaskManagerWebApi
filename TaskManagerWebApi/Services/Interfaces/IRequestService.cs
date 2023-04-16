using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models.DTO;

namespace TaskManagerWebApi.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRequestService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="projectId"></param>
        /// <param name="roleId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Task PostRequestAsync(int studentId, int projectId, int roleId, UserDTO user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IEnumerable<RequestDTO>> GetProjectRequestsAsync(int projectId, int roleId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task AcceptProjectRequestStudentAsync(int projectId, int studentId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task RejectProjectRequestsAsync(int projectId, int studentId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        Task AcceptProjectRequestManagerAsync(int projectId, int studentId);

    }
}
