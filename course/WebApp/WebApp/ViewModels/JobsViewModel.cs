using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class JobsViewModel
    {
        public List<Job> Jobs { get; set; } = new List<Job>();
        public int? CategoryId { get; set; }
        public int? LocationId { get; set; }

        [Range(0, int.MaxValue)]
        public int? MinSalary { get; set; }

        [Range(0, int.MaxValue)]
        public int? MinExpirience { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Location> Locations { get; set; } = new List<Location>();
    }
}
