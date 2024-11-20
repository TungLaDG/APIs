namespace APIs.Model
{
    public class Purchases
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int PurchasePrice { get; set; }
        public int Quantity { get; set; }
        public int TotalCost { get; set; }
    }
}
