using Server.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<CourseModel> favCourses { get; set; }
    }
}
