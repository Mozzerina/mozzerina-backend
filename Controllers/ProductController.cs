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
            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.SubProductId == id);
            
            if(product == null)
            {
                return NotFound("Product not found");
            }

            return Ok(product);
        }
    }
}
