namespace APIs.Model
{
    public class SaleDetailsDto
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }
        public decimal SubTotal { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string ProductName { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
