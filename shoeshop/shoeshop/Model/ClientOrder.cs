using Microsoft.VisualBasic;
using System;

namespace shoeshop.Model
{
    public class ClientOrder
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string PaymentMethod { get; set; }
    }
}
