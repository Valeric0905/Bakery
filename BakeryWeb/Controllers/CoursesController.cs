using Microsoft.AspNetCore.Mvc;

namespace BakeryWeb.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
