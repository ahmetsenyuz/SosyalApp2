using SosyalApp2.Core.Models;

namespace SosyalApp2.Core.Interfaces
{
    public interface ITaskService
    {
        Task<List<Task>> GetDailyTasksForUserAsync(int userId);
        Task<List<Task>> GetTasksByDifficultyAsync(string difficultyLevel);
        Task<Task?> GetTaskDetailsAsync(int taskId);
        Task<bool> UpdateTaskStatusAsync(int taskId, string status);
        Task<List<DailyTaskAssignment>> GetUserTaskHistoryAsync(int userId);
    }
}