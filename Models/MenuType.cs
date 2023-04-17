using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mozzerina.Models
{
    public class MenuType
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Href { get; set; }
        public required string ImagePreview { get; set; }
    }
}
