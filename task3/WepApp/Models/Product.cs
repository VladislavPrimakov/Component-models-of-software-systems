using System.ComponentModel.DataAnnotations;

namespace WepApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Count { get; set; }

        [Required]
        public DateOnly ExpirationDate { get; set; }

        [Required]
        public int PlacementId {  get; set; }
        public Placement? Placement { get; set; }
    }
}
