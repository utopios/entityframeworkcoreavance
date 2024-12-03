using democqrs.Models;
using democqrs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace democqrs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductQueryService _productQueryService;
        private readonly ProductCommandService _productCommandService;

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            return await _productQueryService.GetProductAsyncById(id);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)

        {
            await _productCommandService.CreateProductAsync(product);
            return NoContent();
        }

    }
}
