using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Data;
using TaskManagerApi.DAL;
using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerApi.Services.Interfaces;
using TaskManagerWebApi.Models;

namespace TaskManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IJwtService _jwtService;

        public AuthenticateController(IUserService userService, UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager, IJwtService jwtService)
        {
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;   
        }

        /// <summary>
        /// Вход пользователя в систему
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _userService.Authenticate(model);

            return Ok(response);
        }

        /// <summary>
        /// Регистрация пользователя в системе
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var response = await _userService.Register(model, Request, Url);
            
            return Ok(response);
        }


        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest(new { message = "Invalid email confirmation link" });
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) 
                return BadRequest(new { message = "Invalid email parameter" });

            var result = await _userManager.ConfirmEmailAsync(user, code);

            var status = result.Succeeded ? "Thank you for your confirming email" : "Your email is not confirm, please try again later";

            return Ok(status);
        }


        /// <summary>
        /// Восстановление пароля (отправляется ссылка на почту)
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendResetLink")]
        public async Task<IActionResult> SendResetLink([FromQuery] string email)
        {
            if (email.IsNullOrEmpty())
            {
                return BadRequest(new { message = "Invalid email" });
            }

            await _userService.RequestPasswordReset(email);

            return Ok();
        }

        /// <summary>
        /// Создание нового пароля
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ChangePasswordModel model)
        {
            var result = await _userService.ResetPassword(model);

            if (!result)
            {
                return BadRequest(new { message = "An error occurred while resetting your password. Please try again later."});
            }

            return Ok();
        }



        [Authorize]
        [HttpGet("getuser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _jwtService.GetUserByToken(HttpContext);
            return user == null ? NotFound() : Ok(user);
        }
    }
}
