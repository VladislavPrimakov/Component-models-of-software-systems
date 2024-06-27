using System.ComponentModel.DataAnnotations;

namespace WepApp.Models
{
    public class Placement
    {
        public int PlacementId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public List<Product>? Products { get; set; }
    }
}
