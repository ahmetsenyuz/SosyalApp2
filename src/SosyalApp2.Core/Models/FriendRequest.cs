using System.ComponentModel.DataAnnotations;

namespace SosyalApp2.Core.Models
{
    public class FriendRequest
    {
        public int Id { get; set; }

        [Required]
        public int RequesterId { get; set; }
        
        [Required]
        public int ReceiverId { get; set; }
        
        public bool IsAccepted { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? AcceptedAt { get; set; }
    }
}