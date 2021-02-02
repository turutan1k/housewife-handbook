using DataBase.Core;
using Microsoft.EntityFrameworkCore;
using Server.Data.Models;
using Server.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;

namespace DataBase.Services
{
    public class CoursesService : ICourseService
    {
        private readonly IRepository<CourseModel> _courseRepository;
        public CoursesService(IRepository<CourseModel> courseRepository)
        {

            _courseRepository = courseRepository;
            
        }

        IEnumerable<CourseModel> ICourseService.Courses => _courseRepository.Get(course => course.available).ToList();

        public IEnumerable<CourseModel> Courses()
        {
            return _courseRepository.Get(course => course.available).ToList();
        }
        public IEnumerable<CourseModel> GetFavourite()
        {
            return _courseRepository.Get(course => course.isFavourite).ToList();
        }

        public IEnumerable<CourseModel> GetBreakfast()
        {
            return _courseRepository.Get(course => course.isBreakfast).ToList();
        }

        public IEnumerable<CourseModel> GetLunch()
        {
            return _courseRepository.Get(course => course.isLunch).ToList();

        }

        public IEnumerable<CourseModel> GetDinner()
        {
            return _courseRepository.Get(course => course.isDinner).ToList();

        }
    }
}
