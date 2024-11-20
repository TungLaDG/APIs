using APIs.Model;
using APIs.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var cate = await _categoryRepository.GetAllCategoriesAsync();
            return Ok(cate);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>GetCategoryById(int id)
        {
            var cate = await _categoryRepository.GetCategoryByIdAsync(id);
            return cate == null ? NotFound() : Ok(cate);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Categories category)
        {
            await _categoryRepository.AddCategoryAsync(category);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateCategory(int id, [FromBody] Categories category)
        {
            category.Id = id;
            await _categoryRepository.UpdateCategoryAsync(category);
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteCategory(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
            return Ok();
        }
    }
}
