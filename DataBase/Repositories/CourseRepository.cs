using DataBase.Services;
using Server.Data.Models;

namespace Server.Data.Repository
{
    
    public class CourseRepository : BaseRepository<CourseModel>
    {
        public CourseRepository(AppDBContext context) : base(context)
        {
        
        }
    }
}
