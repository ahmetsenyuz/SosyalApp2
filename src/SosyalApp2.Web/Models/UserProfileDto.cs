using System.ComponentModel.DataAnnotations;

namespace SosyalApp2.Web.Models
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Bio { get; set; } = string.Empty;
        
        public string ProfilePictureUrl { get; set; } = string.Empty;
        
        public int TotalPoints { get; set; }
        
        public int CompletedTasksCount { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
    }
}