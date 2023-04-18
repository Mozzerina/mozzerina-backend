using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mozzerina.Models
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        [ForeignKey(nameof(MenuType))]
        public required int IdMenuType { get; set; }
        public MenuType MenuType { get; set; }
    }
}
