using System.ComponentModel.DataAnnotations;

namespace Task5.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        [Required]
        public double BaseCost { get; set; }

        public List<Risk>? Risks { get; set;} = new List<Risk>();
    }
}
