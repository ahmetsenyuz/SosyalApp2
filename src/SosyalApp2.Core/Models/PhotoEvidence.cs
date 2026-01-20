using System.ComponentModel.DataAnnotations;

namespace SosyalApp2.Core.Models
{
    public class PhotoEvidence
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FileName { get; set; } = string.Empty;

        [StringLength(500)]
        public string FilePath { get; set; } = string.Empty;

        public int TaskId { get; set; }
        
        public DateTime UploadDate { get; set; }
        
        public string? Description { get; set; }
    }
}