using Server.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.ViewModels
{
    public class CoursesListViewModel
    {
        public IEnumerable<CourseModel> allCourses { get; set; }
        public string currCategory { get; set; }
    }
}
