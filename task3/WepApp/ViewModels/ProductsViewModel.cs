using WepApp.Models;

namespace WepApp.ViewModels
{
    public class ProductsViewModel
    {
        public bool InStock { get; set; } = true;
        public DateOnly? ExpiredDate { get; set; }
        public int? PlacementId { get; set; }

        public List<Product>? Products { get; set; }
        public List<Placement>? Placements { get; set; }
    }
}
