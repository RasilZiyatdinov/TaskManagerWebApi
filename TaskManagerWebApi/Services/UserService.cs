using TaskManagerApi.DAL;
using TaskManagerApi.Models;
using TaskManagerApi.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagerApi.Entities;
using RestSharp.Authenticators;
using RestSharp;
using System.Security.Policy;
using TaskManagerWebApi.Services;
using Microsoft.AspNetCore.WebUtilities;
using TaskManagerWebApi.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using TaskManagerApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IConfiguration _configuration;
        ApplicationDbContext _context;

        //private Logger _logger = LogManager.GetCurrentClassLogger();
        private static ILogger<UserService> logger;
        public UserService(UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IConfiguration configuration,
            ILogger<UserService> _logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<AuthenticateResponse> Authenticate(LoginModel model)
        {
            var user = await _userManager.Users.Include(x => x.Group).FirstOrDefaultAsync(x => x.Email == model.Email);


            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                //logger.LogWarning($"Пользователь с логином {request.Email} не найден");
                throw new AppException("Email or password is incorrect");
            }

            if (!user.EmailConfirmed)
            {
                throw new AppException("Email is not verified");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
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
                    UserName = model.FullName,
                    GroupId = model.GroupId,
                    EmailConfirmed = false
                };
            }
            else
            {
                user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.FullName,
                    EmailConfirmed = false
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
    }
}
