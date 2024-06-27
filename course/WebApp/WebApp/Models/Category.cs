using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;

        public List<Resume>? Resumes { get; set; }
        public List<Job>? Jobs { get; set; }
    }
}
