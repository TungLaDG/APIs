namespace APIs.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
    }
}
