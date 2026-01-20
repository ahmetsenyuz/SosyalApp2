using SosyalApp2.Core.Models;

namespace SosyalApp2.Core.Interfaces
{
    public interface IPhotoEvidenceRepository
    {
        Task<PhotoEvidence?> GetPhotoEvidenceByIdAsync(int id);
        Task<IEnumerable<PhotoEvidence>> GetPhotoEvidencesByTaskIdAsync(int taskId);
        Task<PhotoEvidence> CreatePhotoEvidenceAsync(PhotoEvidence photoEvidence);
        Task<bool> DeletePhotoEvidenceAsync(int id);
    }
}