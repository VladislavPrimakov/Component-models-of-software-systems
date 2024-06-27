using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Job
    {
        public int JobId { get; set; }

        [Required]
        public string Title {  get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, int.MaxValue)]
        public int Salary { get; set; }

        [Range(0, int.MaxValue)]
        public int? MinExpirience { get; set; }

        [Required]
        public DateOnly PostedAt { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public int EmployerId { get; set; }
        public Employer? Employer { get; set; }

        [Required]
        public int LocationId { get; set; }
        public Location? Location { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
