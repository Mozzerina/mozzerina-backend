namespace Mozzerina.Models
{
    public class Drink
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Url { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}
