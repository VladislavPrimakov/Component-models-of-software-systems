using System.ComponentModel.DataAnnotations;

namespace Task5.Models
{
    public class Risk
    {
        public int RiskId { get; set; }

        [Required]
        public double Probability { get; set; }

        [Required]
        public double Impact { get; set; }

        [Required]
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
