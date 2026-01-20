using System.ComponentModel.DataAnnotations;

namespace SosyalApp2.Core.Models
{
    public class DailyTaskAssignment
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TaskId { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime AssignedDate { get; set; }

        public DateTime? CompletedDate { get; set; }
    }
}