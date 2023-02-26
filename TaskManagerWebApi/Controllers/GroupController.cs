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
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupService"></param>
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var response = await _groupService.GetGroupsAsync();

            if (response == null)
                return BadRequest(new { message = "Error" });

            return Ok(response);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post(Group g)
        {
            var response = await _groupService.AddGroupAsync(g);

            if (response == null)
                return BadRequest(new { message = "Error" });

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Group g)
        {
            var updateResult = await _groupService.UpdateGroupAsync(g);
            return Ok(updateResult);
        }

        [HttpDelete("delete/{id}")]
        public async Task<bool> Delete(long id)
        {
            return await _groupService.DeleteGroupAsync(id);
        }
    }
}
