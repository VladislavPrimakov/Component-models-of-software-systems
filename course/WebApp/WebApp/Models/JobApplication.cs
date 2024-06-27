using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class JobApplication
    {
        public int JobApplicationId { get; set; }

        [Required]
        public int JobApplicationStatusId { get; set; }
        public JobApplicationStatus? JobApplicationStatus { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public int ResumeId { get; set; }
        public Resume? Resume { get; set; }

        [Required]
        public int JobId { get; set; }
        public Job? Job { get; set; }
    }
}
