using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class JobApplicationStatus
    {
        public int JobApplicationStatusId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
