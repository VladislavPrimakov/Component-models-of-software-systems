using WebApp.Models;

namespace WebApp.ViewModels
{
    public class ResumeViewModel
    {
        public Resume? Resume { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Location> Locations { get; set; } = new List<Location>();
    }
}
