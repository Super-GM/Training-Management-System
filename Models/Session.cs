using System.ComponentModel.DataAnnotations.Schema;

namespace Training_Management_System.Models
{
    public class Session
    {
        public int id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("Course")]
        public int courseid { get; set; }
        public Course course { get; set; }
        public ICollection<Grade> grades { get; set; }
    }
}
