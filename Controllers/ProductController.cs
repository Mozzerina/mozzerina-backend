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

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductRequest request)
        {
            var subproduct = await _dataContext.SubProducts.FirstOrDefaultAsync(s => s.Name == request.Name);

            var product = new Product
            {
                Name = subproduct.Name,
                Image = subproduct.Image,
                Description = request.Product.Description,
                Ingredients = request.Product.Ingredients,
                Allergens = request.Product.Allergens,
                Nutrition = request.Product.Nutrition,
                Callories = request.Product.Callories,
                Short = request.Product.Callories,
                Tall = (int)(request.Product.Callories * 1.5),
                Grande = (int)(request.Product.Callories * 2),
                Venti = (int)(request.Product.Callories * 2.5),
                SubProductId = subproduct.Id
            };

            await _dataContext.Products.AddAsync(product);

            await _dataContext.SaveChangesAsync();

            return Ok(product);
        }
    }
}
