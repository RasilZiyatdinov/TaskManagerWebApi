using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models.DTO;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerWebApi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_configuration"></param>
        /// <param name="_userManager"></param>
        /// <param name="_roleManager"></param>
        public JwtService(IConfiguration _configuration, UserManager<User> _userManager, RoleManager<IdentityRole<int>> _roleManager)
        {
            userManager = _userManager;
            roleManager = _roleManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HttpContext"></param>
        /// <returns></returns>
        public async Task<UserDTO> GetUserByToken(HttpContext HttpContext)
        {
            var claim = HttpContext.User.Claims.First(c => c.Type == "id");
            var id = claim.Value;
            claim = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Role);
            var roleName = claim.Value;

            var role = roleManager.Roles.Where(r => r.Name == roleName).First();           
            var user = await userManager.Users.Include(x => x.Group).FirstAsync(x => x.Id == int.Parse(id));

            return new UserDTO(user, new RoleDTO { Id = role.Id, Name = role.Name });
        }
    }
}
