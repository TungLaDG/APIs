namespace APIs.Model
{
    public class Sale
    {
        public int ID { get; set; }
        public DateTime SaleDate { get; set; }
        public int TotalAmount { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomerName { get; set; }
    }
}
