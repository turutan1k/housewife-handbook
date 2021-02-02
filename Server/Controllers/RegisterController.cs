using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }
}