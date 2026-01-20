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
        private readonly ITaskCompletionService _taskCompletionService;

        public TaskController(ITaskService taskService, ITaskCompletionService taskCompletionService)
        {
            _taskService = taskService;
            _taskCompletionService = taskCompletionService;
        }

        // /// <summary>
        // /// Gets the daily tasks assigned to the authenticated user
        // /// </summary>
        // /// <param name="userId">The ID of the user</param>
        // /// <returns>List of assigned tasks</returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetDailyTasksForUser(int userId)
        {
            // In a real implementation, you would verify that the user is authenticated
            // and that they have permission to view this user's profile

            var tasks = await _taskService.GetDailyTasksForUserAsync(userId);

            return Ok(tasks);
        }

        // /// <summary>
        // /// Gets tasks filtered by difficulty level
        // /// </summary>
        // /// <param name="difficultyLevel">The difficulty level to filter by</param>
        // /// <returns>List of tasks with the specified difficulty</returns>
        [HttpGet("difficulty/{difficultyLevel}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasksByDifficulty(string difficultyLevel)
        {
            var tasks = await _taskService.GetTasksByDifficultyAsync(difficultyLevel);
            return Ok(tasks);
        }

        // /// <summary>
        // /// Gets detailed information about a specific task
        // /// </summary>
        // /// <param name="taskId">The ID of the task</param>
        // /// <returns>Task details</returns>
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

        // /// <summary>
        // /// Updates the status of a task
        // /// </summary>
        // /// <param name="taskId">The ID of the task</param>
        // /// <param name="status">New status for the task</param>
        // /// <returns>Success status</returns>
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

        // /// <summary>
        // /// Gets the task history for a user
        // /// </summary>
        // /// <param name="userId">The ID of the user</param>
        // /// <returns>User's task history</returns>
        [HttpGet("history/{userId}")]
        public async Task<ActionResult<IEnumerable<DailyTaskAssignment>>> GetUserTaskHistory(int userId)
        {
            // In a real implementation, you would verify that the user is authenticated
            // and that they have permission to view this user's task history

            var history = await _taskService.GetUserTaskHistoryAsync(userId);
            return Ok(history);
        }

        // /// <summary>
        // /// Marks a task as completed by the user
        // /// </summary>
        // /// <param name="taskId">The ID of the task</param>
        // /// <param name="userId">The ID of the user completing the task</param>
        // /// <returns>Success status</returns>
        [HttpPost("complete/{taskId}/{userId}")]
        public async Task<ActionResult<bool>> MarkTaskAsCompleted(int taskId, int userId)
        {
            try
            {
                var result = await _taskCompletionService.MarkTaskAsCompletedAsync(taskId, userId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // /// <summary>
        // /// Submits photo evidence for a completed task
        // /// </summary>
        // /// <param name="taskId">The ID of the task</param>
        // /// <param name="userId">The ID of the user submitting evidence</param>
        // /// <param name="fileName">Name of the uploaded file</param>
        // /// <param name="filePath">Path where the file is stored</param>
        // /// <param name="description">Optional description of the evidence</param>
        // /// <returns>Created photo evidence</returns>
        [HttpPost("evidence/{taskId}/{userId}")]
        public async Task<ActionResult<PhotoEvidence>> SubmitPhotoEvidence(int taskId, int userId, [FromForm] string fileName, [FromForm] string filePath, [FromForm] string? description = null)
        {
            try
            {
                var photoEvidence = await _taskCompletionService.SubmitPhotoEvidenceAsync(taskId, userId, fileName, filePath, description);
                return Ok(photoEvidence);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // /// <summary>
        // /// Retrieves photo evidence for a specific task
        // /// </summary>
        // /// <param name="taskId">The ID of the task</param>
        // /// <returns>List of photo evidence for the task</returns>
        [HttpGet("evidence/{taskId}")]
        public async Task<ActionResult<IEnumerable<PhotoEvidence>>> GetPhotoEvidencesForTask(int taskId)
        {
            var photoEvidences = await _taskCompletionService.GetPhotoEvidencesForTaskAsync(taskId);
            return Ok(photoEvidences);
        }
    }
}