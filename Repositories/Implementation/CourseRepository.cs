using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Training_Management_System.Data;
using Training_Management_System.Models;

namespace Training_Management_System.Repositories.Implementation
{
    public class CourseRepository
    {
        private readonly SystemContext _context;
        public CourseRepository(SystemContext context)
        {
            _context = context;
        }
        public IEnumerable<Course> GetAll()
        {
            return _context.courses
                .Include(c => c.instructor)
                .Include(c => c.Sessions)
                .ToList();
        }

        public Course? GetById(int id)
        {
            return _context.courses
                .Include(c => c.instructor)
                .Include(c => c.Sessions)
                .FirstOrDefault(c => c.id == id);
        }

        public IEnumerable<Course> Search(string name, CourseCategory? category)
        {
            var query = _context.courses.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.Name.Contains(name));

            if (category.HasValue)
                query = query.Where(c => c.Category == category.Value);

            return query.ToList();
        }

        public void Add(Course course)
        {
            _context.courses.Add(course);
            _context.SaveChanges();
        }

        public void Update(Course course)
        {
            _context.courses.Update(course);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var course = _context.courses
                .Include(c => c.Sessions)
                .ThenInclude(s => s.grades) // if grades exist
                .FirstOrDefault(c => c.id == id);

            if (course != null)
            {
                // remove dependent children first
                foreach (var session in course.Sessions)
                {
                    _context.grades.RemoveRange(session.grades);
                }

                _context.sessions.RemoveRange(course.Sessions);
                _context.courses.Remove(course);

                _context.SaveChanges();
            }
        }


        public IEnumerable<SelectListItem> GetAllInstructors()
        {
            return _context.users
                .Where(u => u.Role == "Instructor") // change depending on your User model
                .Select(u => new SelectListItem
                {
                    Value = u.id.ToString(),
                    Text = u.Name
                })
                .ToList();
        }
        public User? GetInstructorById(int id)
        {
            return _context.users.FirstOrDefault(u => u.id == id);
        }
    }
}
