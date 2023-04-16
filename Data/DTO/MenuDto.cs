using Mozzerina.Models;

namespace Mozzerina.Data.DTO
{
    public class MenuDto
    {
        public string Name { get; set; } = "Drinks";
        public List<object> Items { get; set; }
    }
}
