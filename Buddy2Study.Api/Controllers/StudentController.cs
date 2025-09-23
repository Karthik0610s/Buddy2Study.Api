using Microsoft.AspNetCore.Mvc;

namespace Buddy2Study.Api.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
