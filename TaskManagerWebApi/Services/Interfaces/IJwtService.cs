using System.IdentityModel.Tokens.Jwt;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Models.DTO;

namespace TaskManagerWebApi.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HttpContext"></param>
        /// <returns></returns>
        Task<UserDTO> GetUserByToken(HttpContext HttpContext);
    }
}
