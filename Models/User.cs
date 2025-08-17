using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Training_Management_System.Models
{
    public partial class User
    {
        public int id { get; set; }
        public string Name { get; set; }
        [EmailAddress (ErrorMessage ="The Email you entered is incorrect")]
        public String Email { get; set; }
        public string Role { get; set; }
        [ValidateNever]
        public ICollection<Course>? courses { get; set; }
        [ValidateNever]
        public ICollection<Grade>? grades { get; set; }
    }
}
