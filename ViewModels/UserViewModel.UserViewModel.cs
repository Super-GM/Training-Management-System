using Training_Management_System.Models;

namespace Training_Management_System.ViewModels
{
    public partial class UserViewModel
    {

        public UserViewModel()
        {
            
     
        }
        public UserViewModel(User user)
        {
            this.id = user.id;
            this.Name = user.Name;
            this.Email = user.Email;
            this.Role = user.Role;
        }
    }
}
