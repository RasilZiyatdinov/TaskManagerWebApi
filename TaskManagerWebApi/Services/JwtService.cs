using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagerApi.DAL;
using TaskManagerApi.Entities;
using TaskManagerApi.Helpers;
using TaskManagerApi.Models;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerApi.Services
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<User> userManager;
        public JwtService(IConfiguration _configuration, UserManager<User> _userManager)
        {
            userManager = _userManager;
        }
        public async Task<UserModel> GetUserByToken(HttpContext HttpContext)
        {
            var claim = HttpContext.User.Claims.First(c => c.Type == "id");
            var id = claim.Value;
            claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role);
            var role = claim.Value;

            var user = await userManager.Users.Include(x => x.Group).FirstOrDefaultAsync(x => x.Id == int.Parse(id));

            return user != null ? new UserModel(user, role) : null;
        }
    }
}
