using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Services;
using TaskManagerWebApi.Models.DTO;
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
        /// Подать заявку
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="projectId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost("addstudent")]
        public async Task<IActionResult> PostStudent(int studentId, int projectId, int roleId)
        {
            UserDTO user = await jwtService.GetUserByToken(HttpContext);
            await requestService.PostRequestAsync(studentId, projectId, roleId, user);
            return Ok();
        }

        /// <summary>
        /// Получить заявки в проект
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
        /// Принять заявку студента в проект (менеджер)
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpPost("acceptstudentrequest")]
        public async Task<IActionResult> AcceptStudentRequest(int projectId, int studentId)
        {
            await requestService.AcceptProjectRequestStudentAsync(projectId, studentId);
            return Ok();
        }

        /// <summary>
        /// Принять заявку менеджера в проект (преподаватель)
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpPost("acceptmanagertrequest")]
        public async Task<IActionResult> AcceptManagerRequest(int projectId, int studentId)
        {
            await requestService.AcceptProjectRequestManagerAsync(projectId, studentId);
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
