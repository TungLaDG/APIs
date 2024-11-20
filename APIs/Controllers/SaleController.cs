using APIs.Model;
using APIs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            var sales = await _saleService.GetAllSalesAsync();
            return Ok(sales);
        }

        //[HttpGet("search")]
        //public async Task<IActionResult> GetSaleByKeyword([FromQuery] string keyword)
        //{
        //    var sales = await _saleService.GetSaleByKeywordAsync(keyword);
        //    return Ok(sales);
        //}
        [HttpGet("search")]
        public async Task<IActionResult> GetSaleByKeyword(string keyword)
        {
            var saleDetails = await _saleService.GetSaleByKeywordAsync(keyword);
            return Ok(saleDetails);
        }
        [HttpPost]
        public async Task<IActionResult> AddSale([FromBody] Sale sale)
        {
            var result = await _saleService.AddSaleAsync(sale);
            if (result > 0) return Ok(new { message = "Sale added successfully." });
            return BadRequest(new { message = "Failed to add sale." });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSale([FromBody] Sale sale)
        {
            var result = await _saleService.UpdateSaleAsync(sale);
            if (result > 0) return Ok(new { message = "Sale updated successfully." });
            return BadRequest(new { message = "Failed to update sale." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var result = await _saleService.DeleteSaleAsync(id);
            if (result > 0) return Ok(new { message = "Sale deleted successfully." });
            return BadRequest(new { message = "Failed to delete sale." });
        }
    }
}
