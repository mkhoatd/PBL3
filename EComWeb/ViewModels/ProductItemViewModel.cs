using ECom.DataAccess.Data;
using ECom.Models;
using ECom.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EComWeb.ViewModels
{
    
    public class ProductItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl {get; set;}
        public decimal Price {get; set;}
    }
}