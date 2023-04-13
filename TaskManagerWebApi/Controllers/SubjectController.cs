using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Entities;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerApi.Controllers
{
    //[Authorize(Roles = "Преподаватель")]
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
        /// <param name="_subjectService"></param>
        /// <param name="_jwtService"></param>
        /// <param name="_logger"></param>
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
            //UserModel user = await jwtService.GetUserByToken(HttpContext);
            var response = await subjectService.GetSubjectByTeacherAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Добавить новую дисциплину - "Преподаватель"
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Post(SubjectModel subject)
        {
            UserModel user = await jwtService.GetUserByToken(HttpContext);
            await subjectService.AddSubjectAsync(subject);
            return Ok();
        }

        /// <summary>
        /// Обновить дисциплину - "Преподаватель"
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Put(SubjectModel subject)
        {
            UserModel user = await jwtService.GetUserByToken(HttpContext);
            await subjectService.UpdateSubjectAsync(subject);
            return Ok();
        }

        /// <summary>
        /// Удалить дисциплину - "Преподаватель"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            //UserModelResponse user = await jwtService.GetUserByToken(HttpContext);
            var response = await subjectService.DeleteSubjectAsync(id);
            return Ok(response);
        }


    }
}
