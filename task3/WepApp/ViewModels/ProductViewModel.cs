using WepApp.Models;

namespace WepApp.ViewModels
{
    public class ProductViewModel
    {
        public List<Placement> Placements { get; set; } = new List<Placement>();
        public Product Product { get; set; } = new Product();
    }
}
