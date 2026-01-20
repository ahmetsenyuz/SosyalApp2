using Microsoft.AspNetCore.Mvc;
using SosyalApp2.Core.Interfaces;
using SosyalApp2.Core.Models;

namespace SosyalApp2.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Gets the daily tasks assigned to the authenticated user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>List of assigned tasks</returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Task>>> GetDailyTasksForUser(int userId)
        {
            // In a real implementation, you would verify that the user is authenticated
            // and that they have permission to view this user's profile
            
            var tasks = await _taskService.GetDailyTasksForUserAsync(userId);
            
            return Ok(tasks);
        }

        /// <summary>
        /// Gets tasks filtered by difficulty level
        /// </summary>
        /// <param name="difficultyLevel">The difficulty level to filter by</param>
        /// <returns>List of tasks with the specified difficulty</returns>
        [HttpGet("difficulty/{difficultyLevel}")]
        public async Task<ActionResult<List<Task>>> GetTasksByDifficulty(string difficultyLevel)
        {
            var tasks = await _taskService.GetTasksByDifficultyAsync(difficultyLevel);
            return Ok(tasks);
        }

        /// <summary>
        /// Gets detailed information about a specific task
        /// </summary>
        /// <param name="taskId">The ID of the task</param>
        /// <returns>Task details</returns>
        [HttpGet("details/{taskId}")]
        public async Task<ActionResult<Task>> GetTaskDetails(int taskId)
        {
            var task = await _taskService.GetTaskDetailsAsync(taskId);
            
            if (task == null)
            {
                return NotFound($"Task not found for task ID: {taskId}");
            }

            return Ok(task);
        }

        /// <summary>
        /// Updates the status of a task
        /// </summary>
        /// <param name="taskId">The ID of the task</param>
        /// <param name="status">New status for the task</param>
        /// <returns>Success status</returns>
        [HttpPut("status/{taskId}")]
        public async Task<ActionResult<bool>> UpdateTaskStatus(int taskId, [FromBody] string status)
        {
            try
            {
                var result = await _taskService.UpdateTaskStatusAsync(taskId, status);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets the task history for a user
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>User's task history</returns>
        [HttpGet("history/{userId}")]
        public async Task<ActionResult<List<DailyTaskAssignment>>> GetUserTaskHistory(int userId)
        {
            // In a real implementation, you would verify that the user is authenticated
            // and that they have permission to view this user's task history
            
            var history = await _taskService.GetUserTaskHistoryAsync(userId);
            return Ok(history);
        }
    }
}