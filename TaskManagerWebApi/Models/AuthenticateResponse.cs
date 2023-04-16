using Microsoft.AspNetCore.Identity;
using TaskManagerWebApi.Entities;

namespace TaskManagerWebApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticateResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Group { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <param name="token"></param>
        public AuthenticateResponse(User user, string role, string token)
        {
            Id = user.Id;
            FullName = user.FullName;            
            Email = user.Email;
            Group = user.Group?.Name;
            Role = role;
            Token = token;
        }
    }
}
