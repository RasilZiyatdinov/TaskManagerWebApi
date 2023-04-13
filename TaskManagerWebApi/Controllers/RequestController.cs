using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    /// <summary>
    /// Заявки
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService requestService;
        private readonly IJwtService jwtService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_requestService"></param>
        /// <param name="_jwtService"></param>
        public RequestController(IRequestService _requestService, IJwtService _jwtService)
        {
            requestService = _requestService;
            jwtService = _jwtService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="projectId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost("addstudent")]
        public async Task<IActionResult> PostStudent(int studentId, int projectId, int roleId)
        {
            UserModel user = await jwtService.GetUserByToken(HttpContext);
            await requestService.RequestByStudentAsync(studentId, projectId, roleId, user);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("getrequests")]
        public async Task<IActionResult> GetProjectRequests(int projectId, int roleId)
        {
            var response = await requestService.GetProjectRequestsAsync(projectId, roleId);
            return Ok(response);
        }

        /// <summary>
        /// Принять заявку в проект
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpPost("acceptrequest")]
        public async Task<IActionResult> AcceptRequest(int projectId, int studentId)
        {
            await requestService.AcceptProjectRequestStudentAsync(projectId, studentId);
            return Ok();
        }

        /// <summary>
        /// Отклонить заявку в проект
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpPost("rejectrequest")]
        public async Task<IActionResult> RejectRequest(int projectId, int studentId)
        {
            await requestService.RejectProjectRequestsAsync(projectId, studentId);
            return Ok();
        }
    }
}
