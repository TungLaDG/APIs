using APIs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GenerateInvoice(string keyword)
        {
            try
            {
                var invoiceData = await _invoiceService.GenerateInvoiceAsync(keyword);
                return File(invoiceData, "application/octet-stream", "Invoice.txt");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("generate-pdf")]
        public async Task<IActionResult> GenerateInvoicePdf(string keyword)
        {
            try
            {
                var invoiceData = await _invoiceService.GenerateInvoicePdfAsync(keyword);
                return File(invoiceData, "application/pdf", "Invoice.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
