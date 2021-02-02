using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}