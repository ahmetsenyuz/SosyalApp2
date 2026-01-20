using SosyalApp2.Core.Interfaces;
using SosyalApp2.Core.Models;

namespace SosyalApp2.Core.Services
{
    public class TaskCompletionService : ITaskCompletionService
    {
        private readonly ITaskService _taskService;
        private readonly IPhotoEvidenceRepository _photoEvidenceRepository;

        public TaskCompletionService(ITaskService taskService, IPhotoEvidenceRepository photoEvidenceRepository)
        {
            _taskService = taskService;
            _photoEvidenceRepository = photoEvidenceRepository;
        }

        public async Task<bool> MarkTaskAsCompletedAsync(int taskId, int userId)
        {
            // First verify that the task belongs to the user
            var task = await _taskService.GetTaskDetailsAsync(taskId);
            if (task == null)
                return false;

            // Update task status to completed
            return await _taskService.UpdateTaskStatusAsync(taskId, "completed");
        }

        public async Task<PhotoEvidence?> SubmitPhotoEvidenceAsync(int taskId, int userId, string fileName, string filePath, string? description = null)
        {
            // Verify that the task belongs to the user
            var task = await _taskService.GetTaskDetailsAsync(taskId);
            if (task == null)
                return null;

            var photoEvidence = new PhotoEvidence
            {
                FileName = fileName,
                FilePath = filePath,
                TaskId = taskId,
                UploadDate = DateTime.UtcNow,
                Description = description
            };

            return await _photoEvidenceRepository.CreatePhotoEvidenceAsync(photoEvidence);
        }

        public async Task<IEnumerable<PhotoEvidence>> GetPhotoEvidencesForTaskAsync(int taskId)
        {
            return await _photoEvidenceRepository.GetPhotoEvidencesByTaskIdAsync(taskId);
        }
    }
}