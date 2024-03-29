﻿using TaskManagerWebApi.DAL;
using TaskManagerWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Services;
using Microsoft.EntityFrameworkCore;
using TaskManagerWebApi.Services.Interfaces;
using TaskManagerWebApi.Models.DTO;
using TaskManagerWebApi.Models.Enums;

namespace TaskManagerWebApi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IConfiguration _configuration;

        private static ILogger<UserService>? logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="configuration"></param>
        /// <param name="_logger"></param>
        public UserService(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, 
            IConfiguration configuration, ILogger<UserService>? _logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            logger = _logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task<AuthenticateResponse> Authenticate(LoginModel model)
        {
            var user = await _userManager.Users.Include(x => x.Group).FirstOrDefaultAsync(x => x.Email == model.Email);


            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                //logger.LogWarning($"Пользователь с логином {request.Email} не найден");
                throw new AppException("Email or password is incorrect");
            }

            //if (!user.EmailConfirmed)
            //{
            //    throw new AppException("Email is not verified");
            //}

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    //new Claim(ClaimTypes.Role, user.Role.Name)
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);
            return new AuthenticateResponse(user, userRoles.ElementAt(0), new JwtSecurityTokenHandler().WriteToken(token));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="request"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task<Response> Register(RegisterModel model, HttpRequest request, IUrlHelper url)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                throw new AppException($"User with email {model.Email} already exist!");

            var role = await _roleManager.FindByNameAsync(model.Role);
            User user;
            if (role.Name != RolesEnum.Teacher)
            {
                user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FullName = model.FullName,
                    GroupId = model.GroupId,
                    EmailConfirmed = false,
                    UserName = model.Email
                };
            }
            else
            {
                user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    FullName = model.FullName,
                    EmailConfirmed = false,
                    UserName = model.Email
                };
            }

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new AppException("User creation failed! Please check user details and try again.");
            
            await _userManager.AddToRoleAsync(user, role.Name);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = url.Action(
                        "ConfirmEmail",
                        "Authenticate",
                        new { userId = user.Id, code = code },
                        protocol: request.Scheme);

            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(model.Email, "Confirm your account",
                $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>Click here</a>");

            return new Response { Status = "Success", Message = "User created successfully" };
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task RequestPasswordReset(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                await RequestPasswordResetLink(user);
                return;
            }

            //logger.LogWarning("Reset password requested for an account that did not exist.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task RequestPasswordResetLink(User user)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var codeBytes = Encoding.UTF8.GetBytes(code);

            //тут нужна ссылка с параметрами email и токен (ссылка отправляется на почту), при нажатии на ссылку открывается форма для ввода нового пароля в клиенте 
            var link = $"link/change-password?email={user.Email}&activationToken={Convert.ToBase64String(codeBytes)}";

            var emailContent = $"Ссылка для сброса пароля: <a href='{link}'>Click here</a>";

            EmailService emailService = new EmailService();

            await emailService.SendEmailAsync(user.Email, "Password Reset", emailContent);

            //logger.LogInformation($"An password reset email was sent to {user.Email}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> ResetPassword(ChangePasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            var codeBytes = Convert.FromBase64String(model.Token);

            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, Encoding.UTF8.GetString(codeBytes), model.Password);

                if (result.Succeeded)
                {
                    var emailContent = $"Your password is reset now";
                    EmailService emailService = new EmailService();

                    await emailService.SendEmailAsync(user.Email, "Password Reset Complete", emailContent);

                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleDTO> GetRoles()
        {
            return _roleManager.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name}).ToList();
        }
    }
}
