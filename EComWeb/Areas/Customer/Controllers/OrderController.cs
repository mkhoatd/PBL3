using Microsoft.AspNetCore.Mvc;

namespace EComWeb.Areas.Customer.Controllers;

public class OrderController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}