using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mozzerina.Models
{
    public class SubProduct
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        //public required string Href { get; set; }
        public required string Image { get; set; }
        [ForeignKey(nameof(ProductType))]
        public required int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}
