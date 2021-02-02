//using Server.Data.Mocks;
//using Server.Data.Models;
//using System;
//using System.Collections.Generic;

//namespace Server.Data.mocks
//{
//    public class MockCourses : IAllCourses { 

//        private readonly ICoursesCategory _categoryCourses = new MockCategory();
//        public IEnumerable<Course> Courses {
//            get {
//                return new List<Course> 
//                {
//                    new Course {
//                        name = "Яичница с беконом",
//                        longDesc = "",
//                        textMuted = "20 min",
//                        img = "/images/eggs.jpg",
//                        isFavourite = false,
//                        available = true,
//                        category = Categories.Easy,
//                    },
//                    new Course 
//                    {
//                        name = "Карбонара",
//                        longDesc = "",
//                        textMuted = "60 min",
//                        img = "/images/pastacarbonara.jpg",
//                        isFavourite = true,
//                        available = true,
//                         category = Categories.Medium },
//                    new Course 
//                    {
//                        name = "Лазанья с творогом и сыром",
//                        longDesc = "",
//                        textMuted = "180 min",
//                        img = "/images/lazania.jpg",
//                        isFavourite = true,
//                        available = true,
//                         category = Categories.Hard },
                    
//                };
//            }
//        }

//        public IEnumerable<Course> getFavCourses { get; set ; }
//        public Course getObjectCar(int courseID)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
