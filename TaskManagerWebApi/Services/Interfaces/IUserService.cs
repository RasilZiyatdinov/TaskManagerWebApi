using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Models.DTO;

namespace TaskManagerWebApi.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AuthenticateResponse> Authenticate(LoginModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="request"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<Response> Register(RegisterModel userModel, HttpRequest request, IUrlHelper url);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task RequestPasswordReset(string email);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> ResetPassword(ChangePasswordModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<RoleDTO> GetRoles();
    }
}
