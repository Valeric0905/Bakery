using Microsoft.AspNetCore.Mvc;

namespace BakeryWeb.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Courses()
        {
            return View();
        }
        public IActionResult Product()
        {
            return View();
        }
        public IActionResult TechCard()
        {
            return View();
        }
        public IActionResult FreeLessons()
        {
            return View();
        }
    }
}
