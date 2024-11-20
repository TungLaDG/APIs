using APIs.Repositories;
using APIs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateQuantity([FromQuery] int productId, [FromQuery] int quantityChange)
        {
            try
            {
                await _inventoryService.UpdateQuantityAsync(productId, quantityChange);
                return Ok("Inventory updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
