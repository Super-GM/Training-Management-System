using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Training_Management_System.Data;
using Training_Management_System.Models;

namespace Training_Management_System.Repositories.Implementation
{
    public class SessionRepository
    {
        private readonly SystemContext _context;

        public SessionRepository(SystemContext context)
        {
            _context = context;
        }

        public IEnumerable<Session> GetAll()
        {
            return _context.sessions.Include(s => s.course).ToList();
        }

        public Session? GetById(int id)
        {
            return _context.sessions
                .Include(s => s.course)
                .FirstOrDefault(s => s.id == id);
        }

        public IEnumerable<Session> SearchByCourseName(string courseName)
        {
            return _context.sessions
                .Include(s => s.course)
                .Where(s => s.course.Name.Contains(courseName))
                .ToList();
        }

        public void Add(Session session)
        {
            _context.sessions.Add(session);
            _context.SaveChanges();
        }

        public void Update(Session session)
        {
            _context.sessions.Update(session);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var session = _context.sessions.Find(id);
            if (session != null)
            {
                _context.sessions.Remove(session);
                _context.SaveChanges();
            }
        }


        public IEnumerable<SelectListItem> GetAllCourses()
        {
            return _context.courses
                .Select(c => new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = c.Name
                })
                .ToList();
        }

        public Course? GetCourseById(int id)
        {
            return _context.courses.FirstOrDefault(c => c.id == id);
        }

    }
}

