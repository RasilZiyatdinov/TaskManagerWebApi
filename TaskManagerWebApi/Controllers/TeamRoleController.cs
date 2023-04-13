using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    //[Authorize(Roles = "Менеджер")]
    [Route("[controller]")]
    [ApiController]
    public class TeamRoleController : ControllerBase
    {
        private readonly ITeamRoleService _teamRoleService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamRoleService"></param>
        public TeamRoleController(ITeamRoleService teamRoleService)
        {
            _teamRoleService = teamRoleService;
        }

        /// <summary>
        /// Получить все роли
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var response = await _teamRoleService.GetTeamRolesAsync();
            if (response == null)
                return BadRequest(new { message = "Error" });
            return Ok(response);
        }

        /// <summary>
        /// Получить роли в проекте
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("getprojectroles")]
        public async Task<IActionResult> GetProjectRoles(int projectId)
        {
            var response = await _teamRoleService.GetProjectTeamRolesAsync(projectId);
            if (response == null)
                return BadRequest(new { message = "Error" });
            return Ok(response);
        }


        /// <summary>
        /// Добавить роль в проект (разработчик, аналитик и тд) - "Менеджер"
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost("addroletoproject")]
        public async Task<IActionResult> AddRoleToProject(int projectId, string roleName)
        {
            await _teamRoleService.AddRolesToProjectAsync(projectId, roleName);
            return Ok();
        }

        /// <summary>
        /// Удалить роль из проекта - "Менеджер"
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpDelete("deleterolefromproject")]
        public async Task<IActionResult> DeleteRoleFromProject(int projectId, string roleName)
        {
            await _teamRoleService.DeleteRoleFromProjectAsync(projectId, roleName);
            return Ok();
        }
    }
}
