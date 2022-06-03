using System.ComponentModel.DataAnnotations;

namespace ECom.Models
{
    public class BasketItem
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product {get; set;}
        public decimal UnitPrice { get; set; }
        public int Quantity {get; set;}
        public int BasketId {get; set;}
        public Basket? Basket { get; set; }
        public void AddUnits(int quantity)
        {
            Quantity+=quantity;
        }
        public void SetQuantity(int quantity)
        {
            Quantity=quantity;
        }

    }
}