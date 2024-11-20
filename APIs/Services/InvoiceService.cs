
using APIs.Repositories;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Layout.Properties;

namespace APIs.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ISaleRepository _repository;

        public InvoiceService(ISaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<byte[]> GenerateInvoiceAsync(string keyword)
        {
            var salesDetails = await _repository.GetSaleByKeywordAsync(keyword);

            if (!salesDetails.Any())
            {
                throw new Exception("No sales details found for the provided keyword.");
            }

            using var memoryStream = new MemoryStream();
            using var writer = new StreamWriter(memoryStream);

            writer.WriteLine("*********** INVOICE ***********");
            writer.WriteLine($"Customer: {salesDetails.First().CustomerName}");
            writer.WriteLine($"Phone: {salesDetails.First().PhoneNumber}");
            writer.WriteLine($"Date: {salesDetails.First().SaleDate:dd/MM/yyyy}");
            writer.WriteLine("-------------------------------");
            writer.WriteLine("Product\tQty\tPrice\tSubTotal");

            decimal total = 0;

            foreach (var detail in salesDetails)
            {
                writer.WriteLine($"{detail.ProductName}\t{detail.Quantity}\t{detail.SalePrice:C}\t{detail.SubTotal:C}");
                total += detail.SubTotal;
            }

            writer.WriteLine("-------------------------------");
            writer.WriteLine($"Total:\t\t\t{total:C}");
            writer.Flush();

            return memoryStream.ToArray();
        }
        public async Task<byte[]> GenerateInvoicePdfAsync(string keyword)
        {
            var salesDetails = await _repository.GetSaleByKeywordAsync(keyword);

            if (!salesDetails.Any())
            {
                throw new Exception("No sales details found for the provided keyword.");
            }

            using var memoryStream = new MemoryStream();
            using var pdfWriter = new PdfWriter(memoryStream);
            var pdfDocument = new PdfDocument(pdfWriter);
            var document = new Document(pdfDocument);

            // Font chữ đậm
            var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            var title = new Paragraph("*********** INVOICE ***********")
                .SetFont(boldFont)
                .SetFontSize(18)
                .SetTextAlignment(TextAlignment.CENTER);

            document.Add(title);

            document.Add(new Paragraph($"Customer: {salesDetails.First().CustomerName}"));
            document.Add(new Paragraph($"Phone: {salesDetails.First().PhoneNumber}"));
            document.Add(new Paragraph($"Date: {salesDetails.First().SaleDate:dd/MM/yyyy}"));
            document.Add(new Paragraph("\n"));

            var table = new Table(4);
            table.AddHeaderCell(new Cell().Add(new Paragraph("Product").SetFont(boldFont)));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Quantity").SetFont(boldFont)));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Sale Price").SetFont(boldFont)));
            table.AddHeaderCell(new Cell().Add(new Paragraph("SubTotal").SetFont(boldFont)));

            decimal total = 0;
            foreach (var detail in salesDetails)
            {
                table.AddCell(detail.ProductName);
                table.AddCell(detail.Quantity.ToString());
                table.AddCell(detail.SalePrice.ToString("C"));
                table.AddCell(detail.SubTotal.ToString("C"));
                total += detail.SubTotal;
            }

            document.Add(table);
            document.Add(new Paragraph("\n"));
            document.Add(new Paragraph($"Total: {total:C}").SetFont(boldFont));

            document.Close();
            return memoryStream.ToArray();
        }

    }
}
