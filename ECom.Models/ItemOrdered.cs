using Microsoft.EntityFrameworkCore;

namespace ECom.Models
{
    [Owned]
    public class ItemOrdered
    {
        public int ItemId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
    }
}