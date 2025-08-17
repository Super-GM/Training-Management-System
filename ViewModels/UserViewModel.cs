using System.ComponentModel.DataAnnotations;

namespace Training_Management_System.ViewModels
{
    public partial class UserViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "The Email you entered is incorrect")]
        public String Email { get; set; }
        public string Role { get; set; }
    }
}
