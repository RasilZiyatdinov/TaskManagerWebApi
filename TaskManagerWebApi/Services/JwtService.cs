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
using TaskManagerApi.Services.Interfaces;

namespace TaskManagerApi.Services
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        public JwtService(IConfiguration _configuration, UserManager<User> _userManager)
        {
            userManager = _userManager;
            configuration = _configuration;
        }
        public async Task<UserModelResponse> GetUserByToken(HttpContext HttpContext)
        {
            var claim = HttpContext.User.Claims.First(c => c.Type == "id");
            var id = claim.Value;
            claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role);
            var role = claim.Value;

            var user = await userManager.Users.Include(x => x.Group).FirstOrDefaultAsync(x => x.Id == int.Parse(id));

            return user != null ? new UserModelResponse(user, role) : null;
            //return null;
        }
    }
}
