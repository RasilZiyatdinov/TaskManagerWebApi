using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;
using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerWebApi.Models;

namespace TaskManagerApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(LoginModel model);
        Task<Response> Register(RegisterModel userModel, HttpRequest request, IUrlHelper url);
        Task RequestPasswordReset(string email);

        Task<bool> ResetPassword(ChangePasswordModel model);

    }
}
