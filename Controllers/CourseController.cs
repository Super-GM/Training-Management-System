using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Training_Management_System.Models;
using Training_Management_System.Repositories.Implementation;
using Training_Management_System.ViewModels;

namespace Training_Management_System.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseRepository _courseRepo;

        public CourseController(CourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public async Task<IActionResult> Index(string name, CourseCategory? category)
        {
            var query = _courseRepo.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.Name.Contains(name));

            if (category.HasValue)
                query = query.Where(c => c.Category == category);

            var courses = query.ToList();

            // جهز الكاتيجوريز للـ dropdown
            ViewBag.Categories = Enum.GetValues(typeof(CourseCategory))
                .Cast<CourseCategory>()
                .Select(c => new SelectListItem
                {
                    Value = c.ToString(),
                    Text = c.ToString(),
                    Selected = category.HasValue && c == category.Value
                }).ToList();

            ViewBag.Name = name;

            return View(courses);
        }


        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                var vm = new CourseViewModel
                {
                    Instructors = _courseRepo.GetAllInstructors()
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error while creating form: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public IActionResult Create(CourseViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    vm.Instructors = _courseRepo.GetAllInstructors();
                    return View(vm);
                }

                var course = new Course
                {
                    Name = vm.Name,
                    Category = vm.Category,
                    instructorid = vm.InstructorId
                };

                _courseRepo.Add(course);
                TempData["Success"] = "Course created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error while creating course: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var course = _courseRepo.GetById(id);
                if (course == null)
                {
                    TempData["Error"] = "Course not found";
                    return RedirectToAction(nameof(Index));
                }

                var vm = new CourseViewModel
                {
                    Id = course.id,
                    Name = course.Name,
                    Category = course.Category,
                    InstructorId = course.instructorid,
                    Instructors = _courseRepo.GetAllInstructors()
                };

                return View(vm);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error while editing form: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, CourseViewModel vm)
        {
            try
            {
                if (id != vm.Id)
                {
                    TempData["Error"] = "Invalid course ID.";
                    return RedirectToAction(nameof(Index));
                }

                if (!ModelState.IsValid)
                {
                    vm.Instructors = _courseRepo.GetAllInstructors();
                    return View(vm);
                }

                var course = _courseRepo.GetById(id);
                if (course == null)
                {
                    TempData["Error"] = "Course not found.";
                    return RedirectToAction(nameof(Index));
                }

                course.Name = vm.Name;
                course.Category = vm.Category;
                course.instructorid = vm.InstructorId;

                _courseRepo.Update(course);
                TempData["Success"] = "Course updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error while updating course: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _courseRepo.Delete(id);
                TempData["Success"] = "Course deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error while deleting course: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
