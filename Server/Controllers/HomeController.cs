using Microsoft.AspNetCore.Mvc;
using Server.ViewModels;
using DataBase.Services;
using DataBase.Core;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Server.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Web.CustomPolicyProvider;

namespace Server.Controllers
{
    //
    public class HomeController : Controller
    {
        private readonly ICourseService _courseRep;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthorizationService _authorisationService;

        public HomeController(IAuthorizationService authorizationService,
            ICourseService courseRep,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _authorisationService = authorizationService;
            _courseRep = courseRep;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public ViewResult Index()
        {
            var homeCourses = new HomeViewModel
            {
                favCourses = _courseRep.GetFavourite()
            };
            return View(homeCourses);
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Policy = "Claim.DnB")]
        //[Authorize(Policy = "SecurityLevel.5")]

        public IActionResult SecretPolicy()
        {
            return View("Secret");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult SecretRole()
        {
            return View("Secret");
        }

        [SecurityLevel(5)]
        public IActionResult SecretLevel()//
        {
            return View("Secret");
        }

        [SecurityLevel(10)]
        public IActionResult SecretHigherLevel()//
        {
            return View("Secret");
        }

        [AllowAnonymous]
        public IActionResult Authenticate()//
        {
            var grandmaClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Email, "Bob@gmail.com"),
                new Claim(ClaimTypes.DateOfBirth, "11/11/2000"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(DynamicPolicies.SecurityLevel, "7"),

                new Claim("Grandma.Says", "Very nice boy."),
            };

            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob K Foo"),
                new Claim("DrivingLicense", "A+"),
            };

            var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Goverment");

            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity, licenseIdentity });
            return RedirectToAction("Index");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Login(string email, string password)
        {
            //login function
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                //sign in
                var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string firstname, string password)
        {
            //register functionality
            var user = new IdentityUser
            {
                UserName = firstname,
                Email = "",
            };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                //sign user there
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> DoStuff(
            [FromServices] IAuthorizationService authorizationService)
        {
            //we are doing stuff here

            var builder = new AuthorizationPolicyBuilder("Schema");
            var customPolicy = builder.RequireClaim("Hello").Build();

            var authResut = await authorizationService.AuthorizeAsync(HttpContext.User, customPolicy);
            if (authResut.Succeeded )
            {
                return View("Index");
            }
                return View("Index");
        }
    }
}
