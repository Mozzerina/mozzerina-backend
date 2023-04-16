using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Mozzerina.Data;
using Mozzerina.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Mozzerina.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MozzerinaContext _dataContext;
        private readonly IHostEnvironment _hostEnvironment;
        public DbInitializer(MozzerinaContext dataContext, IHostEnvironment hostEnvironment)
        {
            _dataContext = dataContext;
            _hostEnvironment = hostEnvironment;
        }
        public string ImageSrc(string img)
        {
            string imagePath = Path.Combine(_hostEnvironment.ContentRootPath, img);
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            string base64String = Convert.ToBase64String(imageBytes);
            string imgSrc = string.Format("data:image/jpeg;base64,{0}", base64String);
            
            return imgSrc;
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
                        Url = "/menu/drinks/hot-coffees",
                        Image = ImageSrc("Images/Menu/frappuccino.png")
                    },
                    new Drink
                    {
                        Name = "Гарячі чаї",
                        Url = "/menu/drinks/hot-teas",
                        Image = ImageSrc("Images/Menu/hotTea.png")
                    },
                    new Drink
                    {
                        Name = "Гарячі напої",
                        Url = "/menu/drinks/hot-drinks",
                        Image = ImageSrc("Images/Menu/hotDrink.png")
                    },
                    new Drink
                    {
                        Name = "Frappuccino",
                        Url = "/menu/drinks/frappuccino-blended-beverages",
                        Image = ImageSrc("Images/Menu/frappuccino.png")
                    },
                    new Drink
                    {
                        Name = "Холодна кава",
                        Url = "/menu/drinks/cold-coffees",
                        Image = ImageSrc("Images/Menu/coldCoffee.png")
                    },
                    new Drink
                    {
                        Name = "Холодні чаї",
                        Url = "/menu/drinks/iced-teas",
                        Image = ImageSrc("Images/Menu/icedTea.png")
                    },
                    new Drink
                    {
                        Name = "Холодні напої",
                        Url = "/menu/drinks/cold-drinks",
                        Image = ImageSrc("Images/Menu/coldDrink.png")
                    }
                    );
            }
            if (!_dataContext.Foods.Any())
            {
                _dataContext.Foods.AddRange(
                    new Food
                    {
                        Name = "Гарячий сніданок",
                        Url = "/menu/food/hot-breakfast",
                        Image = ImageSrc("Images/Food/hot-breakfast.png")
                    },
                    new Food
                    {
                        Name = "Пекарня",
                        Url = "/menu/food/bakery",
                        Image = ImageSrc("Images/Food/backery.png")
                    },
                    new Food
                    {
                        Name = "Обід",
                        Url = "/menu/food/lunch",
                        Image = ImageSrc("Images/Food/lunch.png")
                    },
                    new Food
                    {
                        Name = "Закуски та солодощі",
                        Url = "/menu/food/snacks-sweets",
                        Image = ImageSrc("Images/Food/snack.png")
                    },
                    new Food
                    {
                        Name = "Вівсянка та йогурт",
                        Url = "/menu/food/oatmeal-yogurt",
                        Image = ImageSrc("Images/Food/yogurt.png")
                    }
                    );
            }
            if (!_dataContext.AtHomeCoffees.Any())
            {
                _dataContext.AtHomeCoffees.AddRange(
                    new AtHomeCoffee
                    {
                        Name = "VIA Instant",
                        Url = "/menu/at-home-coffee/via-instant",
                        Image = ImageSrc("Images/AtHomeCoffee/via.png")
                    },
                    new AtHomeCoffee
                    {
                        Name = "Ціла квасоля",
                        Url = "/menu/at-home-coffee/whole-bean",
                        Image = ImageSrc("Images/AtHomeCoffee/wholebean.png")
                    }
                    );
            }
            if (!_dataContext.Merchandises.Any())
            {
                _dataContext.Merchandises.AddRange(
                    new Merchandise
                    {
                        Name = "Холодні чашки",
                        Url = "/menu/merchandise/cold-cups",
                        Image = ImageSrc("Images/Merchandise/coldcup.png")
                    },
                    new Merchandise
                    {
                        Name = "Стакани",
                        Url = "/menu/merchandise/tumblers",
                        Image = ImageSrc("Images/Merchandise/Tumblers.png")
                    },
                    new Merchandise
                    {
                        Name = "Кружки",
                        Url = "/menu/merchandise/mugs",
                        Image = ImageSrc("Images/Merchandise/Mugs.png")
                    },
                    new Merchandise
                    {
                        Name = "Пляшки з водою",
                        Url = "/menu/merchandise/water-bottles",
                        Image = ImageSrc("Images/Merchandise/WaterBottles.png")
                    },
                    new Merchandise
                    {
                        Name = "Інші",
                        Url = "/menu/merchandise/other",
                        Image = ImageSrc("Images/Merchandise/other.png")
                    }
                    );
            }
            if (!_dataContext.GiftCards.Any())
            {
                _dataContext.GiftCards.AddRange(
                    new GiftCard
                    {
                        Name = "З днем народження",
                        Url = "/menu/gift-cards/happy-birthday",
                        Image = ImageSrc("Images/GiftCard/happy.png")
                    },
                    new GiftCard
                    {
                        Name = "Дякую",
                        Url = "/menu/gift-cards/thank-you",
                        Image = ImageSrc("Images/GiftCard/thank.png")
                    },
                    new GiftCard
                    {
                        Name = "Традиційні",
                        Url = "/menu/gift-cards/traditional",
                        Image = ImageSrc("Images/GiftCard/Traditional.png")
                    }
                    );
            }
            _dataContext.SaveChanges();
        }
    }
}
