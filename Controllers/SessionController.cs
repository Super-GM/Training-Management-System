using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Training_Management_System.Data;
using Training_Management_System.Models;
using Training_Management_System.Repositories.Implementation;
using Training_Management_System.ViewModels;

namespace Training_Management_System.Controllers
{
    public class SessionController : Controller
    {
        private readonly SessionRepository _sessionRepo;

        public SessionController(SessionRepository sessionRepo)
        {
            _sessionRepo = sessionRepo;
        }

        public IActionResult Index(string search)
        {
            var sessions = string.IsNullOrEmpty(search)
                ? _sessionRepo.GetAll()
                : _sessionRepo.SearchByCourseName(search);

            return View(sessions);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Courses = _sessionRepo.GetAllCourses();
            return View();
        }
        [HttpPost]
        public IActionResult Create(SessionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = _sessionRepo.GetAllCourses();
                return View(model);
            }

            var session = new Session
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                courseid = model.CourseId
            };

            _sessionRepo.Add(session);
            TempData["Success"] = "Session created successfully!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var session = _sessionRepo.GetById(id);
            if (session == null) return NotFound();

            var model = new SessionViewModel
            {
                Id = session.id,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                CourseId = session.courseid
            };

            ViewBag.Courses = _sessionRepo.GetAllCourses();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(SessionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = _sessionRepo.GetAllCourses();
                return View(model);
            }

            var session = _sessionRepo.GetById(model.Id);
            if (session == null)
            {
                TempData["Error"] = "Session not found!";
                return NotFound();
            }

            session.StartDate = model.StartDate;
            session.EndDate = model.EndDate;
            session.courseid = model.CourseId;

            _sessionRepo.Update(session);
            TempData["Success"] = "Session updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _sessionRepo.Delete(id);
                TempData["Success"] = "Session deleted successfully!";
            }
            catch
            {
                TempData["Error"] = "Error deleting session!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
