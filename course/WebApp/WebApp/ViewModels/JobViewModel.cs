using WebApp.Models;

namespace WebApp.ViewModels
{
    public class JobViewModel
    {
        public Job Job { get; set; } = new Job();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Location> Locations { get; set; } = new List<Location>();
    }
}
