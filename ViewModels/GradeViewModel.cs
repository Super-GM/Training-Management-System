using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Training_Management_System.ViewModels
{
    public class GradeViewModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Session is required")]
        [Display(Name = "Session")]
        public int Sessionid { get; set; }

        [Required(ErrorMessage = "Trainee is required")]
        [Display(Name = "Trainee")]
        public int Traineeid { get; set; }

        [Required(ErrorMessage = "Value is required")]
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100")]
        [Display(Name = "Grade")]
        public decimal Value { get; set; }

        public IEnumerable<SelectListItem> Sessions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Trainees { get; set; } = new List<SelectListItem>();

        public string? CourseName { get; set; }
        public string? TraineeName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
