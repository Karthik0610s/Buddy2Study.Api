using Microsoft.AspNetCore.Mvc;

namespace Buddy2Study.Api.Controllers
{
    public class SponsorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
