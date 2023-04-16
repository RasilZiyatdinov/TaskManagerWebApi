using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Models.DTO;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
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
        /// Получить участников проекта
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("getparticipants")]
        public async Task<IActionResult> GetParticipants(int projectId)
        {
            var response = await projectService.GetProjectParticipants(projectId);
            return Ok(response);
        }

        /// <summary>
        /// Получить проекты преподавателя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyteacher")]
        public IActionResult GetByTeacher(int id)
        {
            var response = projectService.GetProjectsByTeacherAsync(id);
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
        /// 
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet("getbysubjectandgroup")]
        public async Task<IActionResult> GetBySubjectAndGroup(int subjectId, int groupId)
        {
            var response = await projectService.GetProjectsByGroupSubjAsync(groupId, subjectId);
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
            UserDTO? user = await jwtService.GetUserByToken(HttpContext);
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
            UserDTO? user = await jwtService.GetUserByToken(HttpContext);
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
            UserDTO? user = await jwtService.GetUserByToken(HttpContext);
            var response = await projectService.DeleteProjectAsync(id, user);
            return Ok(response);
        }

       
    }
}
