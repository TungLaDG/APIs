namespace APIs.Services
{
    public interface IInvoiceService
    {
        Task<byte[]> GenerateInvoiceAsync(string keyword);
        Task<byte[]> GenerateInvoicePdfAsync(string keyword);

    }
}
