using System.ComponentModel.DataAnnotations;

namespace Training_Management_System.ViewModels
{
    public class SessionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(SessionViewModel), nameof(ValidateStartDate))]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(SessionViewModel), nameof(ValidateEndDate))]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Course is required")]
        public int CourseId { get; set; }

        public string? CourseName { get; set; }

        // Custom Validation
        public static ValidationResult? ValidateStartDate(DateTime startDate, ValidationContext context)
        {
            if (startDate < DateTime.Today)
                return new ValidationResult("Start date cannot be in the past.");
            return ValidationResult.Success;
        }

        public static ValidationResult? ValidateEndDate(DateTime endDate, ValidationContext context)
        {
            var instance = context.ObjectInstance as SessionViewModel;
            if (instance != null && endDate <= instance.StartDate)
                return new ValidationResult("End date must be after the start date.");
            return ValidationResult.Success;
        }
    }
}
