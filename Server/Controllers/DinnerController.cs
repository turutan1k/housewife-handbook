using DataBase.Core;
using Microsoft.AspNetCore.Mvc;
using Server.ViewModels;

namespace Server.Controllers
{
    public class DinnerController : Controller
    {
        private readonly ICourseService _courseRep;

        public DinnerController(ICourseService courseRep)
        {
            _courseRep = courseRep;
        }
        public ViewResult Dinner()
        {
            var dinnerCourses = new DinnerViewModel
            {
                dinnerCourses = _courseRep.GetDinner()
            };
            return View(dinnerCourses);

        }
    }
}
