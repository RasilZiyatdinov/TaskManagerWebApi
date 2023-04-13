using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;
using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Models.DTO;

namespace TaskManagerWebApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(LoginModel model);
        Task<Response> Register(RegisterModel userModel, HttpRequest request, IUrlHelper url);
        Task RequestPasswordReset(string email);

        Task<bool> ResetPassword(ChangePasswordModel model);
        Task<IEnumerable<RoleDTO>> GetRoles();

    }
}
