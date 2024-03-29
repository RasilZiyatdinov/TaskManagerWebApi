﻿using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskService"></param>
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Добавить задачу - "Менеджер"
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> Post(TaskModel t)
        {
            await _taskService.AddTaskAsync(t);
            return Ok();
        }

        /// <summary>
        /// Обновить задачу - "Менеджер"
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPut("updatebymanager")]
        public async Task<IActionResult> Update(TaskModel t)
        {
            await _taskService.UpdateTaskAsync(t);
            return Ok();
        }


        /// <summary>
        /// Удалить задачу - "Менеджер"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<bool> Delete(int id)
        {
            return await _taskService.DeleteTaskAsync(id);
        }

        /// <summary>
        /// Получить все задачи в проекте
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("getbyproject")]
        public IActionResult GetAllTasks(int projectId)
        {
            var response =  _taskService.GetAllTasksAsync(projectId);
            return Ok(response);
        }

        /// <summary>
        /// Получить задачи по роли
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("getbyrole")]
        public IActionResult GetTasksByRole(int roleId, int projectId)
        {
            var response =  _taskService.GetTasksByRoleAsync(roleId, projectId);
            return Ok(response);
        }

        /// <summary>
        /// Получить задачи студента
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("getbystudent")]
        public IActionResult GetTasksByStudent(int studentId, int projectId)
        {
            var response = _taskService.GetTasksByStudentAsync(studentId, projectId);
            return Ok(response);
        }

        /// <summary>
        /// Обновить задачу (занести время) - "Студент"
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        [HttpPut("updatebystudent")]
        public async Task<IActionResult> UpdateTaskByStudent(TaskStudentModel t)
        {
            await _taskService.UpdateTaskByStudentAsync(t);
            return Ok();
        }
    }
}
