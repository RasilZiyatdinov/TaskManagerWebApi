
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Services;
using TaskManagerApi.DAL;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerWebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class StatusController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Статусы, приоритеты
        /// </summary>
        /// <param name="_dbContext"></param>
        public StatusController(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /// <summary>
        /// Получить статусы задач (открыта, в работе, решена, ...)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTaskStatuses")]
        public async Task<IActionResult> GetTaskStatus()
        {
            var response = await dbContext.Status.ToListAsync();
            if (response == null)
                return BadRequest(new { message = "Error" });
            return Ok(response);
        }

        /// <summary>
        /// Получить приоритеты задач (низкий, высокий, ...)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTaskPriorities")]
        public async Task<IActionResult> GetTaskPriority()
        {
            var response = await dbContext.Priority.ToListAsync();
            if (response == null)
                return BadRequest(new { message = "Error" });
            return Ok(response);
        }


        /// <summary>
        /// Получить статусы заявок
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRequestStatuses")]
        public async Task<IActionResult> GetRequestStatus()
        {
            var response = await dbContext.RequestStatus.ToListAsync();
            if (response == null)
                return BadRequest(new { message = "Error" });
            return Ok(response);
        }
    }
}
