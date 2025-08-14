using System.ComponentModel.DataAnnotations.Schema;

namespace Training_Management_System.Models
{
    public class Grade
    {
        public int id { get; set; }
        public decimal Value { get; set; }
        [ForeignKey("Session")]
        public int Sessionid { get; set; }
        public Session session { get; set; }

        [ForeignKey("Trainee")]
        public int Traineeid { get; set; }
        public User Trainee { get; set; }
    }
}
