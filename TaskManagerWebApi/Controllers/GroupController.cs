using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Entities;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var response = await _groupService.GetGroupsAsync();

            if (response == null)
                return BadRequest(new { message = "Error" });

            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Post(Group g)
        {
            var response = await _groupService.AddGroupAsync(g);

            if (response == null)
                return BadRequest(new { message = "Error" });

            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update(Group g)
        {
            var updateResult = await _groupService.UpdateGroupAsync(g);
            return Ok(updateResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<bool> Delete(long id)
        {
            return await _groupService.DeleteGroupAsync(id);
        }
    }
}
