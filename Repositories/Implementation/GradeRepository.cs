using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Training_Management_System.Data;
using Training_Management_System.Models;

namespace Training_Management_System.Repositories.Implementation
{
    public class GradeRepository
    {
        private readonly SystemContext _context;

        public GradeRepository(SystemContext context)
        {
            _context = context;
        }

        public IEnumerable<Grade> GetAll(string? traineeName = null, int? sessionId = null)
        {
            var q = _context.grades
                .Include(g => g.session)
                    .ThenInclude(s => s.course)
                .Include(g => g.Trainee)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(traineeName))
                q = q.Where(g => g.Trainee.Name.Contains(traineeName));

            if (sessionId.HasValue)
                q = q.Where(g => g.Sessionid == sessionId.Value);

            return q
                .OrderBy(g => g.Sessionid)
                .ThenBy(g => g.Traineeid)
                .ToList();
        }

        public Grade? GetById(int id)
        {
            return _context.grades
                .Include(g => g.session)
                    .ThenInclude(s => s.course)
                .Include(g => g.Trainee)
                .FirstOrDefault(g => g.id == id);
        }

        public bool Exists(int sessionId, int traineeId, int? excludeId = null)
        {
            return _context.grades.Any(g =>
                g.Sessionid == sessionId &&
                g.Traineeid == traineeId &&
                (!excludeId.HasValue || g.id != excludeId.Value));
        }

        public void Add(Grade grade)
        {
            _context.grades.Add(grade);
            _context.SaveChanges();
        }

        public void Update(Grade grade)
        {
            _context.grades.Update(grade);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.grades.Find(id);
            if (entity != null)
            {
                _context.grades.Remove(entity);
                _context.SaveChanges();
            }
        }

        public List<SelectListItem> GetSessionsSelectList()
        {
            return _context.sessions
                .Include(s => s.course)
                .OrderBy(s => s.StartDate)
                .Select(s => new SelectListItem
                {
                    Value = s.id.ToString(),
                    Text = s.course != null
                        ? $"{s.course.Name} — {s.StartDate:yyyy-MM-dd} → {s.EndDate:yyyy-MM-dd}"
                        : $"Session #{s.id}"
                })
                .ToList();
        }

        public List<SelectListItem> GetTraineesSelectList()
        {
            return _context.users
                .Where(u => u.Role == "Trainee")
                .OrderBy(u => u.Name)
                .Select(u => new SelectListItem
                {
                    Value = u.id.ToString(),
                    Text = $"{u.Name} ({u.Email})"
                })
                .ToList();
        }
    }
}
