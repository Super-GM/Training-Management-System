using Microsoft.EntityFrameworkCore;
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
            var user = _context.users
                .Include(u => u.courses)
                    .ThenInclude(c => c.Sessions)
                        .ThenInclude(s => s.grades)
                .FirstOrDefault(u => u.id == id);

            if (user != null)
            {
                foreach (var course in user.courses.ToList())
                {
                    foreach (var session in course.Sessions.ToList())
                    {
                        _context.grades.RemoveRange(session.grades);

                        _context.sessions.Remove(session);
                    }

                    _context.courses.Remove(course);
                }

                _context.users.Remove(user);

                _context.SaveChanges();
            }
        }



    }
}
