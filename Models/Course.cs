using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Training_Management_System.Models
{
    public enum CourseCategory
    {
        Programming,
        Chemistry,
        Physics,
        Biology,
        Astronomy
    }
    public class Course
    {
        public int id { get; set; }
        public string Name { get; set; }
        public CourseCategory Category { get; set; }
        public ICollection<Session> Sessions { get; set; }
        [ForeignKey("Instructor")]
        public int instructorid { get; set; }
        public User instructor { get; set; }
    }
}
