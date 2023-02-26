using Microsoft.AspNetCore.Identity;
using TaskManagerApi.Entities;

namespace TaskManagerApi.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string? Group { get; set; }


        public AuthenticateResponse(User user, string role, string token)
        {
            Id = user.Id;
            FullName = user.UserName;            
            Email = user.Email;
            Group = user.Group?.Name;
            Role = role;
            Token = token;
        }
    }
}
