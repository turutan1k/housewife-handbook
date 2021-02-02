using Server.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.ViewModels
{
    public class LunchViewModel
    {
        public IEnumerable<CourseModel> lunchCourses { get; set; }
    }
}
