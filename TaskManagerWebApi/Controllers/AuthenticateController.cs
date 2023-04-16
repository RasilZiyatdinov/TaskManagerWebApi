using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagerWebApi.DAL;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    /// <summary>
    /// Авторизация и аутентификация
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IJwtService _jwtService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="jwtService"></param>
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

            Response.Cookies.Append("X-Access-Token", response.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append("X-Email", response.Email, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            //Response.Cookies.Append("X-Refresh-Token", user.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

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

        /// <summary>
        /// Подтверждение email (переход по ссылке)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
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
            return Ok(result);
        }


        /// <summary>
        /// Получить пользователя по токену
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("getuser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _jwtService.GetUserByToken(HttpContext);
            return user == null ? NotFound() : Ok(user);
        }

        /// <summary>
        /// Получить роли в системе (преподаватель, студент, менеджер)
        /// </summary>
        /// <returns></returns>
        [HttpGet("getroles")]
        public IActionResult GetRoles()
        {
            return Ok(_userService.GetRoles());
        }
    }
}
