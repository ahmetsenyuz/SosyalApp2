using SosyalApp2.Core.Models;

namespace SosyalApp2.Core.Interfaces
{
    public interface ITaskCompletionService
    {
        Task<bool> MarkTaskAsCompletedAsync(int taskId, int userId);
        Task<PhotoEvidence?> SubmitPhotoEvidenceAsync(int taskId, int userId, string fileName, string filePath, string? description = null);
        Task<IEnumerable<PhotoEvidence>> GetPhotoEvidencesForTaskAsync(int taskId);
    }
}