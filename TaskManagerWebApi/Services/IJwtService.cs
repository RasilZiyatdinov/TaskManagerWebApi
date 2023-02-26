using System.IdentityModel.Tokens.Jwt;
using TaskManagerApi.Entities;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services.Interfaces
{
    public interface IJwtService
    {
        Task<UserModelResponse> GetUserByToken(HttpContext HttpContext);
    }
}
