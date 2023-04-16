using Mozzerina.Models;
using Newtonsoft.Json;

namespace Mozzerina.Data.DTO
{
    public class MenuDto
    {
        [JsonProperty("Напої")]
        public List<Drink> Drink { get; set; } = new List<Drink>();

        [JsonProperty("Їжа")]
        public List<Food> Food { get; set; } = new List<Food>();

        [JsonProperty("Домашня кава")]
        public List<AtHomeCoffee> AtHomeCoffee { get; set; } = new List<AtHomeCoffee>();

        [JsonProperty("Товари")]
        public List<Merchandise> Merchandise { get; set; } = new List<Merchandise>();

        [JsonProperty("Подарункові картки")]
        public List<GiftCard> GiftCard { get; set; } = new List<GiftCard>();
    }
}
