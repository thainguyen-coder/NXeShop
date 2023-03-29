using Microsoft.AspNetCore.Mvc;

namespace NXeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        // GET: Admin/Home/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}
