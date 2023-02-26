using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerApi.Services.Interfaces;

namespace TaskManagerApi.Controllers
{
    [Authorize(Roles = "Преподаватель")]
    [Route("[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService subjectService;
        private readonly IJwtService jwtService;
        private static ILogger<SubjectController> logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subjectService"></param>
        public SubjectController(ISubjectService _subjectService, IJwtService _jwtService, ILogger<SubjectController> _logger)
        {
            subjectService = _subjectService;
            jwtService = _jwtService;
            logger = _logger;
        }

        /// <summary>
        /// Получить дисциплины преподавателя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyteacher")]
        public async Task<IActionResult> GetByTeacher(int id)
        {
            UserModelResponse user = await jwtService.GetUserByToken(HttpContext);
            var response = await subjectService.GetSubjectByTeacherAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Добавить новую дисциплину
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Post(SubjectModel subject)
        {
            UserModelResponse user = await jwtService.GetUserByToken(HttpContext);
            var response = await subjectService.AddSubjectAsync(subject);
            return Ok(response);
        }

        /// <summary>
        /// Обновить дисциплину
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Put(SubjectModel subject)
        {
            UserModelResponse user = await jwtService.GetUserByToken(HttpContext);
            var response = await subjectService.UpdateSubjectAsync(subject);
            return Ok(response);
        }

        /// <summary>
        /// Удалить дисциплину
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            UserModelResponse user = await jwtService.GetUserByToken(HttpContext);
            var response = await subjectService.DeleteSubjectAsync(id);
            return Ok(response);
        }


    }
}
