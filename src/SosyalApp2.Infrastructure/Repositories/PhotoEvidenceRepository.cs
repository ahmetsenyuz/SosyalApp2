using Microsoft.EntityFrameworkCore;
using SosyalApp2.Core.Interfaces;
using SosyalApp2.Core.Models;

namespace SosyalApp2.Infrastructure.Repositories
{
    public class PhotoEvidenceRepository : IPhotoEvidenceRepository
    {
        private readonly AppDbContext _context;

        public PhotoEvidenceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PhotoEvidence?> GetPhotoEvidenceByIdAsync(int id)
        {
            return await _context.PhotoEvidences.FindAsync(id);
        }

        public async Task<IEnumerable<PhotoEvidence>> GetPhotoEvidencesByTaskIdAsync(int taskId)
        {
            return await _context.PhotoEvidences
                .Where(pe => pe.TaskId == taskId)
                .ToListAsync();
        }

        public async Task<PhotoEvidence> CreatePhotoEvidenceAsync(PhotoEvidence photoEvidence)
        {
            _context.PhotoEvidences.Add(photoEvidence);
            await _context.SaveChangesAsync();
            return photoEvidence;
        }

        public async Task<bool> DeletePhotoEvidenceAsync(int id)
        {
            var photoEvidence = await _context.PhotoEvidences.FindAsync(id);
            if (photoEvidence == null) return false;

            _context.PhotoEvidences.Remove(photoEvidence);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}