using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mozzerina.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public required string Description { get; set; }
        public int Callories { get; set; }
        public int Short { get; set; } = 0;
        public int Tall { get; set; } = 0;
        public int Grande { get; set; } = 0;
        public int Venti { get; set; } = 0;
        public required string Ingredients { get; set; }
        public required string Allergens { get; set; }
        public required string Nutrition { get; set; }
        [ForeignKey(nameof(SubProduct))]
        public required int SubProductId {get;set;}
        public SubProduct SubProduct { get; set;}
    }
}
