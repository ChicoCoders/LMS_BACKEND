using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    public class RequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
