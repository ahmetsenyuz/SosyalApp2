using SosyalApp2.Core.Interfaces;
using SosyalApp2.Core.Models;

namespace SosyalApp2.Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly Random _random = new Random();
        private static readonly List<Task> _tasks = new()
        {
            new Task
            {
                Id = 1,
                Title = "Morning Meditation",
                Description = "10 minutes of mindfulness meditation in the morning",
                DifficultyLevel = "Easy",
                Status = "Available",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Task
            {
                Id = 2,
                Title = "Read 10 Pages",
                Description = "Read 10 pages of a book of your choice",
                DifficultyLevel = "Easy",
                Status = "Available",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Task
            {
                Id = 3,
                Title = "Write Journal Entry",
                Description = "Write a journal entry about your day",
                DifficultyLevel = "Easy",
                Status = "Available",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Task
            {
                Id = 4,
                Title = "Learn New Skill",
                Description = "Learn a new skill for 30 minutes",
                DifficultyLevel = "Medium",
                Status = "Available",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Task
            {
                Id = 5,
                Title = "Complete Project Task",
                Description = "Work on a project task for 1 hour",
                DifficultyLevel = "Medium",
                Status = "Available",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Task
            {
                Id = 6,
                Title = "Code Review",
                Description = "Review code for a pull request",
                DifficultyLevel = "Hard",
                Status = "Available",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Task
            {
                Id = 7,
                Title = "Research Topic",
                Description = "Research a topic for 45 minutes",
                DifficultyLevel = "Hard",
                Status = "Available",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        private static readonly List<DailyTaskAssignment> _taskAssignments = new();

        public async Task<List<Task>> GetDailyTasksForUserAsync(int userId)
        {
            await Task.Delay(1); // Simulate async operation

            // In a real implementation, this would:
            // 1. Check if user already has assigned tasks for today
            // 2. If not, assign 3 random tasks from different difficulty levels
            // 3. Return the assigned tasks
            
            var availableTasks = _tasks.Where(t => t.Status == "Available").ToList();
            
            // Ensure we have at least 3 tasks
            if (availableTasks.Count < 3)
            {
                return availableTasks;
            }

            // Select one task from each difficulty level
            var easyTasks = availableTasks.Where(t => t.DifficultyLevel == "Easy").ToList();
            var mediumTasks = availableTasks.Where(t => t.DifficultyLevel == "Medium").ToList();
            var hardTasks = availableTasks.Where(t => t.DifficultyLevel == "Hard").ToList();

            var selectedTasks = new List<Task>();

            if (easyTasks.Any()) selectedTasks.Add(easyTasks[_random.Next(easyTasks.Count)]);
            if (mediumTasks.Any()) selectedTasks.Add(mediumTasks[_random.Next(mediumTasks.Count)]);
            if (hardTasks.Any()) selectedTasks.Add(hardTasks[_random.Next(hardTasks.Count)]);

            // If we don't have 3 tasks, fill with random ones
            while (selectedTasks.Count < 3 && availableTasks.Any())
            {
                var randomTask = availableTasks[_random.Next(availableTasks.Count)];
                if (!selectedTasks.Contains(randomTask))
                {
                    selectedTasks.Add(randomTask);
                }
            }

            // Record assignments
            foreach (var task in selectedTasks)
            {
                _taskAssignments.Add(new DailyTaskAssignment
                {
                    Id = _taskAssignments.Count + 1,
                    UserId = userId,
                    TaskId = task.Id,
                    Status = "Assigned",
                    AssignedDate = DateTime.UtcNow
                });
            }

            return selectedTasks;
        }

        public async Task<List<Task>> GetTasksByDifficultyAsync(string difficultyLevel)
        {
            await Task.Delay(1); // Simulate async operation
            return _tasks.Where(t => t.DifficultyLevel == difficultyLevel).ToList();
        }

        public async Task<Task?> GetTaskDetailsAsync(int taskId)
        {
            await Task.Delay(1); // Simulate async operation
            return _tasks.FirstOrDefault(t => t.Id == taskId);
        }

        public async Task<bool> UpdateTaskStatusAsync(int taskId, string status)
        {
            await Task.Delay(1); // Simulate async operation
            
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                task.Status = status;
                task.UpdatedAt = DateTime.UtcNow;
                return true;
            }
            
            return false;
        }

        public async Task<List<DailyTaskAssignment>> GetUserTaskHistoryAsync(int userId)
        {
            await Task.Delay(1); // Simulate async operation
            return _taskAssignments.Where(ta => ta.UserId == userId).ToList();
        }
    }
}