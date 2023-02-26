using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerApi.Services.Interfaces;

namespace TaskManagerApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;
        private readonly IJwtService jwtService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectService"></param>
        public ProjectController(IProjectService _projectService, IJwtService _jwtService)
        {
            projectService = _projectService;
            jwtService = _jwtService;
        }

        /// <summary>
        /// Получить проекты преподавателя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyteacher")]
        public async Task<IActionResult> Get(int teacherId)
        {
            var response = await projectService.GetProjectsByTeacherAsync(teacherId);
            return Ok(response);
        }

        /// <summary>
        /// Добавить новый проект
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Post(Project p)
        {
            UserModelResponse user = await jwtService.GetUserByToken(HttpContext);
            var response = await projectService.AddProjectAsync(p, user);
            return Ok(response);
        }

        /// <summary>
        /// Обновить проект
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update(Project p)
        {
            UserModelResponse user = await jwtService.GetUserByToken(HttpContext);
            var updateResult = await projectService.UpdateProjectAsync(p);
            return Ok(updateResult);
        }

        /// <summary>
        /// Удалить проект
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            UserModelResponse user = await jwtService.GetUserByToken(HttpContext);
            var response = projectService.DeleteProjectAsync(id, user);
            return Ok(response);

        }

        /// <summary>
        /// Выбор проекта менеджером
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost("addmanager")]
        public async Task<IActionResult> PostManager(int managerId, int projectId)
        {
            UserModelResponse user = await jwtService.GetUserByToken(HttpContext);
            var response = await projectService.ChooseProjectByManagerAsync(managerId, projectId, user);
            return Ok(response);
        }

        /// <summary>
        /// Выбор проекта студентом
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost("addstudent")]
        public async Task<IActionResult> PostStudent(int studentId, int projectId)
        {
            UserModelResponse user = await jwtService.GetUserByToken(HttpContext);
            var response = await projectService.ChooseProjectByStudentAsync(studentId, projectId, user);
            return Ok(response);
        }

        /// <summary>
        /// Покинуть проект (менеджер)
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete("deletemanager")]
        public async Task<IActionResult> DeleteManager(int projectId)
        {
            UserModelResponse user = await jwtService.GetUserByToken(HttpContext);
            var response = await projectService.LeaveProjectByManagerAsync(projectId, user);
            return Ok(response);
        }

        /// <summary>
        /// Покинуть проект (студент)
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete("deletestudent")]
        public async Task<IActionResult> DeleteStudent(int studentId, int projectId)
        {
            UserModelResponse user = await jwtService.GetUserByToken(HttpContext);
            var response = await projectService.LeaveProjectByStudentAsync(studentId, projectId, user);
            return Ok(response);
        }
    }
}
