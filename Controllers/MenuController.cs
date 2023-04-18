using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mozzerina.Data;
using Mozzerina.Data.DTO;

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
            var types = await _dataContext.MenuTypes.ToListAsync();

            var drinks = await _dataContext.MenuTypes.Where(u => u.Href.Contains("drinks")).ToListAsync();
            var foods = await _dataContext.MenuTypes.Where(u => u.Href.Contains("food")).ToListAsync();
            var atHome = await _dataContext.MenuTypes.Where(u => u.Href.Contains("at-home-coffee")).ToListAsync();
            var merchandises = await _dataContext.MenuTypes.Where(u => u.Href.Contains("merchandise")).ToListAsync();
            var gift = await _dataContext.MenuTypes.Where(u => u.Href.Contains("gift-cards")).ToListAsync();

            List<ResponseDto> menu = new()
            {
                new ResponseDto {Name = "Напої", Items = drinks.ToList<object>()},
                new ResponseDto {Name = "Харчування", Items = foods.ToList<object>()},
                new ResponseDto {Name = "Домашня кава", Items = atHome.ToList<object>()},
                new ResponseDto {Name = "Товари", Items = merchandises.ToList<object>()},
                new ResponseDto {Name = "Подарункові карти", Items = gift.ToList<object>()},
            };

            return Ok(menu);
        }
    }
}