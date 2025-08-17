using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Training_Management_System.Models;

namespace Training_Management_System.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Course Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public CourseCategory Category { get; set; }
        public ICollection<Session>? Sessions { get; set; }
        [Required(ErrorMessage = "Instructor is required")]
        [Display(Name = "Instructor")]
        public int InstructorId { get; set; }
        public User? Instructor { get; set; }
        public IEnumerable<SelectListItem> Instructors { get; set; } = new List<SelectListItem>();
    }
}
