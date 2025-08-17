using Training_Management_System.Data;
using Training_Management_System.Models;

namespace Training_Management_System.Repositories.Implementation
{
    public class UserRepository
    {
        private readonly SystemContext _context;

        public UserRepository(SystemContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.users.ToList();
        }

        public User? GetById(int id)
        {
            return _context.users.Find(id);
        }

        public void Add(User user)
        {
            _context.users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.users.Find(id);
            if (user != null)
            {
                _context.users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
