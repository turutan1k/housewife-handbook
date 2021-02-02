using Server.Data.Models;
using System.Linq;

namespace Server.Data
{
    public class DBObjects
    {
        public static void Initial(AppDBContext content)
        {
            if (!content.Course.Any())
            {
                content.AddRange();
            }

            content.SaveChanges();
        }
    }
}
