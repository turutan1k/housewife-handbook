using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Data.Models
{
    public class CourseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { set; get; }
        public string name { get; set; }
        public string textMuted { get; set; }
        public string longDesc { get; set; }
        public string img { get; set; }
        public bool isFavourite { get; set; }
        public bool available { set; get; }
        public Categories category { get; set; }
        public bool isBreakfast { get; set; }
        public bool isLunch { get; set; }
        public bool isDinner { get; set; }
    }
}
