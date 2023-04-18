using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mozzerina.Data;
using Mozzerina.Data.DTO;

namespace Mozzerina.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubProductController : ControllerBase
    {
        private readonly MozzerinaContext _dataContext;
        public SubProductController(MozzerinaContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCatalog(string link)
        {
            var menu = await _dataContext.MenuTypes.FirstOrDefaultAsync(m => m.Href == link);
            if(menu == null)
            {
                return NotFound("Incorrect link");
            }

            var types = await _dataContext.ProductTypes.Where(t => t.IdMenuType == menu.Id).ToListAsync();

            List<ResponseDto> response = new List<ResponseDto>();
            foreach (var type in types)
            {
                var subProducts = await _dataContext.SubProducts.Where(k => k.ProductTypeId == type.Id).ToListAsync<object>();
                response.Add(new ResponseDto
                {
                    Name = type.Name,
                    Items = subProducts
                });
            }

            return Ok(response);
        }
    }
}
