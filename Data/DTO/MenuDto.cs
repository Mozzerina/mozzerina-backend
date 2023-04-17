using Mozzerina.Models;

namespace Mozzerina.Data.DTO
{
    public class MenuDto
    {
        public required string Name { get; set; }
        public List<object> Items { get; set; }
    }
}
