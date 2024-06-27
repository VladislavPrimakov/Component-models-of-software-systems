using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class ResumesViewModel
    {
        public List<Resume> Resumes { get; set; } = new List<Resume>();
        public int? CategoryId { get; set; }
        public int? LocationId { get; set; }

        [Range(0, int.MaxValue)]
        public int? MinExperience { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Location> Locations { get; set; } = new List<Location>();
    }
}
