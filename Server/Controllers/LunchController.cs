using DataBase.Core;
using Microsoft.AspNetCore.Mvc;
using Server.ViewModels;

namespace Server.Controllers
{
    public class LunchController : Controller
    {
        private readonly ICourseService _courseRep;

        public LunchController(ICourseService courseRep)
        {
            _courseRep = courseRep;
        }
        public ViewResult Lunch()
        {
            var lunchCourses = new LunchViewModel
            {
                lunchCourses = _courseRep.GetLunch()
            };
            return View(lunchCourses);

        }
    }
}
