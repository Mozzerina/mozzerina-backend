using Microsoft.EntityFrameworkCore;
using Mozzerina.Data;
using Mozzerina.Models;

namespace Mozzerina.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MozzerinaContext _dataContext;
        public DbInitializer(MozzerinaContext dataContext)
        {
            _dataContext = dataContext;
        }
        public void Initialize()
        {
            if (_dataContext.Database.GetPendingMigrations().Count() > 0)
            {
                _dataContext.Database.Migrate();
            }
            if (!_dataContext.Drinks.Any())
            {
                _dataContext.Drinks.AddRange(
                    new Drink
                    {
                        Name = "Гаряча кава",
                        Url = "/menu/drinks/hot-coffees"
                    },
                    new Drink
                    {
                        Name = "Гарячі чаї",
                        Url = "/menu/drinks/hot-teas"
                    },
                    new Drink
                    {
                        Name = "Гарячі напої",
                        Url = "/menu/drinks/hot-drinks"
                    },
                    new Drink
                    {
                        Name = "Frappuccino",
                        Url = "/menu/drinks/frappuccino-blended-beverages"
                    },
                    new Drink
                    {
                        Name = "Холодна кава",
                        Url = "/menu/drinks/cold-coffees"
                    },
                    new Drink
                    {
                        Name = "Холодні чаї",
                        Url = "/menu/drinks/iced-teas"
                    },
                    new Drink
                    {
                        Name = "Холодні напої",
                        Url = "/menu/drinks/cold-drinks"
                    }
                    );
            }
            if (!_dataContext.Foods.Any())
            {
                _dataContext.Foods.AddRange(
                    new Food
                    {
                        Name = "Гарячий сніданок",
                        Url = "/menu/food/hot-breakfast"
                    },
                    new Food
                    {
                        Name = "Пекарня",
                        Url = "/menu/food/bakery"
                    },
                    new Food
                    {
                        Name = "Обід",
                        Url = "/menu/food/lunch"
                    },
                    new Food
                    {
                        Name = "Закуски та солодощі",
                        Url = "/menu/food/snacks-sweets"
                    },
                    new Food
                    {
                        Name = "Вівсянка та йогурт",
                        Url = "/menu/food/oatmeal-yogurt"
                    }
                    );
            }
            if (!_dataContext.AtHomeCoffees.Any())
            {
                _dataContext.AtHomeCoffees.AddRange(
                    new AtHomeCoffee
                    {
                        Name = "VIA Instant",
                        Url = "/menu/at-home-coffee/via-instant"
                    },
                    new AtHomeCoffee
                    {
                        Name = "Ціла квасоля",
                        Url = "/menu/at-home-coffee/whole-bean"
                    }
                    );
            }
            if (!_dataContext.Merchandises.Any())
            {
                _dataContext.Merchandises.AddRange(
                    new Merchandise
                    {
                        Name = "Холодні чашки",
                        Url = "/menu/merchandise/cold-cups"
                    },
                    new Merchandise
                    {
                        Name = "Стакани",
                        Url = "/menu/merchandise/tumblers"
                    },
                    new Merchandise
                    {
                        Name = "Кружки",
                        Url = "/menu/merchandise/mugs"
                    },
                    new Merchandise
                    {
                        Name = "Пляшки з водою",
                        Url = "/menu/merchandise/water-bottles"
                    },
                    new Merchandise
                    {
                        Name = "Інші",
                        Url = "/menu/merchandise/other"
                    }
                    );
            }
            if (!_dataContext.GiftCards.Any())
            {
                _dataContext.GiftCards.AddRange(
                    new GiftCard
                    {
                        Name = "З днем народження",
                        Url = "/menu/gift-cards/happy-birthday"
                    },
                    new GiftCard
                    {
                        Name = "Дякую",
                        Url = "/menu/gift-cards/thank-you"
                    },
                    new GiftCard
                    {
                        Name = "Традиційні",
                        Url = "/menu/gift-cards/traditional"
                    }
                    );
            }
            _dataContext.SaveChanges();
        }
    }
}
