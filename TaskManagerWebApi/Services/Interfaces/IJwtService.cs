using System.IdentityModel.Tokens.Jwt;
using TaskManagerApi.Entities;
using TaskManagerApi.Models;

namespace TaskManagerWebApi.Services.Interfaces
{
    public interface IJwtService
    {
        Task<UserModel> GetUserByToken(HttpContext HttpContext);
    }
}
