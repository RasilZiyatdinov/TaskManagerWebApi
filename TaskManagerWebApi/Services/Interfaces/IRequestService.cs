using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models.DTO;

namespace TaskManagerWebApi.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRequestService
    {
        Task RequestByStudentAsync(int studentId, int projectId, int roleId, UserModel user);
        Task<IEnumerable<RequestDTO>> GetProjectRequestsAsync(int projectId, int roleId);
        Task AcceptProjectRequestStudentAsync(int projectId, int studentId);
        Task RejectProjectRequestsAsync(int projectId, int studentId);

    }
}
