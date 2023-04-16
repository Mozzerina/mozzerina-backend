using Mozzerina.Models;
using Newtonsoft.Json;

namespace Mozzerina.Data.DTO
{
    public class MenuDto
    {
        public Dictionary<string, List<Drink>> Drink { get; set; }
        public Dictionary<string, List<Food>> Food { get; set; }
        public Dictionary<string, List<AtHomeCoffee>> AtHomeCoffee { get; set; }
        public Dictionary<string, List<Merchandise>> Merchandise { get; set; }
        public Dictionary<string, List<GiftCard>> GiftCard { get; set; }
    }
}