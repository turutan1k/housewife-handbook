using DataBase.Core;
using Microsoft.AspNetCore.Mvc;
using Server.ViewModels;

namespace Server.Controllers
{
    public class BreakfastController : Controller
    {
        private readonly ICourseService _courseRep;

        public BreakfastController(ICourseService courseRep)
        {
            _courseRep = courseRep;
        }
        public ViewResult Breakfast()
        {
            var breakfastCourses = new BreakfastViewModel
            {
                breakfastCourses = _courseRep.GetBreakfast()
            };
            return View(breakfastCourses);
        }
    }
}
