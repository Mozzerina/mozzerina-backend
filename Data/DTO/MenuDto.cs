using Mozzerina.Models;

namespace Mozzerina.Data.DTO
{
    public class MenuDto
    {
        public List<Drink> Drink { get; set; } = new List<Drink>();
        public List<Food> Food { get; set; } = new List<Food>();
        public List<AtHomeCoffee> AtHomeCoffee { get; set; } = new List<AtHomeCoffee>();
        public List<Merchandise> Merchandise { get; set; } = new List<Merchandise>();
        public List<GiftCard> GiftCard { get; set; } = new List<GiftCard>();
    }
}
