using APIs.Model;
using APIs.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetAllProductByCategoryId(int Id)
        {
            var products = await _productRepository.GetAllProductsByCategoryIdAsync(Id);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {

            await _productRepository.AddProductAsync(product);
            return Ok(product);
            //return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            //CreatedAtAction cung cấp URL đến tài nguyên mới được tạo, cho phép client có thể dễ dàng truy cập tài nguyên này (thông qua phương thức GetUserById)
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            product.Id = id;

            await _productRepository.UpdateProductAsync(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productRepository.DeleteProductAsync(id);
            return Ok();
        }
    }
}
