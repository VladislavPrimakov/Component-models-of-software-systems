using System.ComponentModel.DataAnnotations;
using WepApp.Models;

namespace WebApp.Models
{
    public class Resume
    {
        public int ResumeId { get; set; }

        [Required]
        public string Title { get; set; } = String.Empty;

        [Required]
        public string Description { get; set; } = String.Empty;

        [Required]
        public DateOnly UpdatedAt { get; set; }

        [Range(0, int.MaxValue)]
        public int? Expirience { get; set; }

        public string? Education { get; set; }

        [Required]
        public bool IsActive { get; set; } = false;

        [Required]
        public int UserId { get; set; }
        
        public int? LocationId { get; set; }
        public Location? Location { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
