using Microsoft.AspNetCore.Mvc;

namespace Buddy2Study.Api.Controllers
{
    public class ScholarshipController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
