using System.ComponentModel.DataAnnotations;

namespace Task5.Models
{
    public class ProjectViewModel
    {
        public List<Project> Projects { get; set; } = new List<Project>();
        [Required]
        [Range(1, int.MaxValue)]
        public int Simulations { get; set; }
        [Required]
        [Range(0,1)]
        public double Confidence { get; set; }
        
        public int? InputDataProjectId { get; set; }
        public double? Result { get; set; }
    }
}
