using APIs.Model;
using APIs.Repositories;
using APIs.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDetailsController : ControllerBase
    {
        private readonly ISaleDetailsService _saleDetailsService;

        public SaleDetailsController(ISaleDetailsService saleDetailsService)
        {
            _saleDetailsService = saleDetailsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSaleDetails()
        {
            var result = await _saleDetailsService.GetAllSaleDetailsAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddsaleDetails([FromBody] SaleDetails saleDetails)
        {
            var result = await _saleDetailsService.AddSaleDetailsAsync(saleDetails);
            if (result > 0) return Ok("Added successfully.");
            return BadRequest("Failed to add.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSaleDetails(int id, [FromBody] SaleDetails saleDetails)
        {
            if (id != saleDetails.ID) return BadRequest("Id not found.");

            var result = await _saleDetailsService.UpdateSaleDetailsAsync(saleDetails);
            if (result > 0) return Ok("Updated successfully.");
            return BadRequest("Failed to update.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleDetails(int id)
        {
            var result = await _saleDetailsService.DeleteSaleDetailsAsync(id);
            if (result > 0) return Ok("Deleted successfully.");
            return BadRequest("Failed to delete.");
        }
        //[HttpGet("search")]
        //public async Task<IActionResult> GetSaleDetailsByKeyword(string keyword)
        //{
        //    var saleDetails = await _saleDetailsService.GetSaleDetailsByKeywordAsync(keyword);
        //    return Ok(saleDetails);
        //}
    }
}
