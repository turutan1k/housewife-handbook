//методы в самом приложении
using Server.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Core
{
    public interface ICourseService
    {
        public IEnumerable<CourseModel> Courses { get; }
        public IEnumerable<CourseModel> GetFavourite();
        public IEnumerable<CourseModel> GetBreakfast();
        public IEnumerable<CourseModel> GetLunch();
        public IEnumerable<CourseModel> GetDinner();
    }
}
