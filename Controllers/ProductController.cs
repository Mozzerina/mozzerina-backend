using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mozzerina.Data;
using Mozzerina.Data.DTO;
using Mozzerina.Models;

namespace Mozzerina.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MozzerinaContext _dataContext;
        public ProductController(MozzerinaContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _dataContext.Products
                .Include(p => p.SubProduct.ProductType.MenuType)
                .Where(p => p.SubProductId == id)
                .Select(p => new
                {
                    product = new
                    {
                        p.Id,
                        p.Name,
                        p.Image,
                        p.Description,
                        p.Callories,
                        p.Short,
                        p.Tall,
                        p.Grande,
                        p.Venti,
                        p.Ingredients,
                        p.Allergens,
                        p.Nutrition,
                    },
                    sizeOpts = p.SubProduct.ProductType.MenuType.Id < 8,
                    link = p.SubProduct.ProductType.MenuType.Href
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound("Product not found");
            }

            return Ok(product);
        }
    }
}
