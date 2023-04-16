using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mozzerina.Data;
using Mozzerina.Data.DTO;
using Newtonsoft.Json;

namespace Mozzerina.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MozzerinaContext _dataContext;
        public MenuController(MozzerinaContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetMenu()
        {
            MenuDto menu = new()
            {
                Drink = await _dataContext.Drinks.ToListAsync(),
                Food = await _dataContext.Foods.ToListAsync(),
                AtHomeCoffee = await _dataContext.AtHomeCoffees.ToListAsync(),
                Merchandise = await _dataContext.Merchandises.ToListAsync(),
                GiftCard = await _dataContext.GiftCards.ToListAsync(),
            };
            string json = JsonConvert.SerializeObject(menu, Formatting.Indented);
            return Ok(json);
        }
    }
}
