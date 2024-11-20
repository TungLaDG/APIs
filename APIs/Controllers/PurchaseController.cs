using APIs.Model;
using APIs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _service;

        public PurchaseController(IPurchaseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPurchases()
        {
            var purchases = await _service.GetAllPurchasesAsync();
            return Ok(purchases);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseById(int id)
        {
            var purchase = await _service.GetPurchaseByIdAsync(id);
            if (purchase == null) return NotFound();
            return Ok(purchase);
        }

        [HttpPost]
        public async Task<IActionResult> AddPurchase([FromBody] Purchases purchase)
        {
            if (purchase == null) return BadRequest("Purchase is null.");

            try
            {
                var result = await _service.AddPurchaseAsync(purchase);
                if (result) return Ok("Purchase added and inventory updated successfully.");
                return BadRequest("Failed to add purchase.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchase(int id, [FromBody] Purchases purchase)
        {
            if (id != purchase.ID) return BadRequest("ID mismatch.");

            try
            {
                var result = await _service.UpdatePurchaseAsync(purchase);
                if (result) return Ok("Purchase updated successfully.");
                return BadRequest("Failed to update purchase.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            try
            {
                var result = await _service.DeletePurchaseAsync(id);
                if (result) return Ok("Purchase deleted successfully.");
                return BadRequest("Failed to delete purchase.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
