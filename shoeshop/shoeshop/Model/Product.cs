namespace shoeshop.Model
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string Color { get; set;}
        public int Size { get; set;}
        public decimal Price { get; set;}
        public int Quantity { get; set;}
        public string ImageName { get; set;}
    }
}
