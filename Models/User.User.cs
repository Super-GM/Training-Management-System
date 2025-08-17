using Training_Management_System.ViewModels;

namespace Training_Management_System.Models
{
    public partial class User
    {
        public User()
        {
            
        }
        public User(UserViewModel UserVM)
        {
            this.id = UserVM.id;
            this.Name = UserVM.Name;
            this.Email = UserVM.Email;
            this.Role = UserVM.Role;
        }
    }
}
