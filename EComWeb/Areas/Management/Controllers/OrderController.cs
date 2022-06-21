using Microsoft.AspNetCore.Mvc;

namespace EComWeb.Areas.Management.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
