using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Services.Interfaces;

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
        /// <param name="_projectService"></param>
        /// <param name="_jwtService"></param>
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
        public async Task<IActionResult> Get(int id)
        {
            var response = await projectService.GetProjectsByTeacherAsync(id);
            return Ok(response);
        }


        /// <summary>
        /// Получить проекты по группе
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbygroup")]
        public async Task<IActionResult> GetByGroup(int id)
        {
            var response = await projectService.GetProjectsByGroupAsync(id);
            return Ok(response);
        }



        /// <summary>
        /// Добавить новый проект - "Преподаватель"
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Post(ProjectModel p)
        {
            UserModel user = await jwtService.GetUserByToken(HttpContext);
            await projectService.AddProjectAsync(p, user);
            return Ok();
        }

        /// <summary>
        /// Обновить проект - "Преподаватель"
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update(ProjectModel p)
        {
            UserModel user = await jwtService.GetUserByToken(HttpContext);
            await projectService.UpdateProjectAsync(p);
            return Ok();
        }

        /// <summary>
        /// Удалить проект - "Преподаватель"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            UserModel user = await jwtService.GetUserByToken(HttpContext);
            var response = await projectService.DeleteProjectAsync(id, user);
            return Ok(response);
        }

       
    }
}
