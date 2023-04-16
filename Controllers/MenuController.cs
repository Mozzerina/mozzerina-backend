﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mozzerina.Data;
using Mozzerina.Data.DTO;
using Mozzerina.Models;
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
            var drink = await _dataContext.Drinks.ToListAsync();
            var food = await _dataContext.Foods.ToListAsync();
            var athomecoffee = await _dataContext.AtHomeCoffees.ToListAsync();
            var merchandise = await _dataContext.Merchandises.ToListAsync();
            var giftcard = await _dataContext.GiftCards.ToListAsync();
            var d = new Dictionary<string, List<Drink>> { { "Drinks", drink } };
            var f = new Dictionary<string, List<Food>> { { "Food", food } };
            var a = new Dictionary<string, List<AtHomeCoffee>> { { "At Home Coffee", athomecoffee } };
            var m = new Dictionary<string, List<Merchandise>> { { "Merchandise", merchandise } };
            var g = new Dictionary<string, List<GiftCard>> { { "Gift Card", giftcard } };

            List<object> obj = new()
            {
                d, f, a, m, g
            };

            return Ok(obj);
        }
    }
}