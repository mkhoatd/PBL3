using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ECom.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [DisplayName("Category")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        [DisplayName("Manufacture")]
        public int? ManufactureId { get; set; }
        public Manufacture? Manufacture { get; set; }
        public Specification? Specification { get; set; }
        public string Description {get; set;}
        [Range(0,100000)]
        public decimal Price { get; set; }
        [Range(0,100)]
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
        public string? ImageUrl{ get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Must have image")]
        public IFormFile Image { get; set; }
        public bool IsDeleted { get; set; }        
    }
}
