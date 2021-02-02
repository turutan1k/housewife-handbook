using Microsoft.AspNetCore.Mvc;
using Server.ViewModels;
using DataBase.Core;
using System.Collections.Generic;
using Server.Data.Models;
using System.Linq;
using System;

namespace Server.Controllers
{
    public class CoursesController : Controller { 
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService) 
        {
            _courseService = courseService;
        }

        [Route("Course/List")]
        [Route("Course/List/{categoryMeals}")]
        public ViewResult List(string categoryMeals) 
        {
            string _categoryMeals = categoryMeals;
            IEnumerable<CourseModel> courses = null;
            string currCategory = "";
            if (string.IsNullOrEmpty(categoryMeals))
            {
                courses = _courseService.Courses.OrderBy(i => i.id);
            }
            else
            {
                if(string.Equals("breakfast", categoryMeals, StringComparison.OrdinalIgnoreCase))
                {
                    courses = _courseService.Courses.Where(i => i.isBreakfast.Equals("breakfast")).OrderBy(i => i.id);
                }
                else if (string.Equals("lunch", categoryMeals, StringComparison.OrdinalIgnoreCase))
                {
                    courses = _courseService.Courses.Where(i => i.isLunch.Equals("lunch")).OrderBy(i => i.id);
                }
                else if (string.Equals("dinner", categoryMeals, StringComparison.OrdinalIgnoreCase))
                {
                    courses = _courseService.Courses.Where(i => i.isDinner.Equals("dinner")).OrderBy(i => i.id);
                }

                currCategory = _categoryMeals;  
        }
            var courseObj = new CoursesListViewModel
            {
                allCourses = courses,
                currCategory = currCategory
            };



            ViewBag.Title = "housewife-handbook";
            return View(courseObj);
        }
    }
}
