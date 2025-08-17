using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Training_Management_System.Models;
using Training_Management_System.Repositories.Implementation;
using Training_Management_System.ViewModels;

namespace Training_Management_System.Controllers
{
    public class GradeController : Controller
    {
        private readonly GradeRepository _gradeRepo;

        public GradeController(GradeRepository gradeRepo)
        {
            _gradeRepo = gradeRepo;
        }

        public IActionResult Index(string? trainee, int? sessionId)
        {
            var data = _gradeRepo.GetAll(trainee, sessionId);

            ViewBag.Sessions = new SelectList(_gradeRepo.GetSessionsSelectList(), "Value", "Text", sessionId);

            return View(data);
        }

        public IActionResult Details(int id)
        {
            var grade = _gradeRepo.GetById(id);
            if (grade == null) return NotFound();
            return View(grade);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new GradeViewModel
            {
                Sessions = _gradeRepo.GetSessionsSelectList(),
                Trainees = _gradeRepo.GetTraineesSelectList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GradeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Sessions = _gradeRepo.GetSessionsSelectList();
                vm.Trainees = _gradeRepo.GetTraineesSelectList();
                return View(vm);
            }

            if (_gradeRepo.Exists(vm.Sessionid, vm.Traineeid))
            {
                ModelState.AddModelError(string.Empty, "A grade for this trainee in this session already exists.");
                vm.Sessions = _gradeRepo.GetSessionsSelectList();
                vm.Trainees = _gradeRepo.GetTraineesSelectList();
                return View(vm);
            }

            var entity = new Grade
            {
                Sessionid = vm.Sessionid,
                Traineeid = vm.Traineeid,
                Value = vm.Value
            };
            _gradeRepo.Add(entity);

            TempData["Success"] = "Grade created successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var grade = _gradeRepo.GetById(id);
            if (grade == null) return NotFound();

            var vm = new GradeViewModel
            {
                id = grade.id,
                Sessionid = grade.Sessionid,
                Traineeid = grade.Traineeid,
                Value = grade.Value,
                Sessions = _gradeRepo.GetSessionsSelectList(),
                Trainees = _gradeRepo.GetTraineesSelectList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, GradeViewModel vm)
        {
            if (id != vm.id) return BadRequest();

            if (!ModelState.IsValid)
            {
                vm.Sessions = _gradeRepo.GetSessionsSelectList();
                vm.Trainees = _gradeRepo.GetTraineesSelectList();
                return View(vm);
            }

            if (_gradeRepo.Exists(vm.Sessionid, vm.Traineeid, excludeId: vm.id))
            {
                ModelState.AddModelError(string.Empty, "A grade for this trainee in this session already exists.");
                vm.Sessions = _gradeRepo.GetSessionsSelectList();
                vm.Trainees = _gradeRepo.GetTraineesSelectList();
                return View(vm);
            }

            var entity = _gradeRepo.GetById(id);
            if (entity == null) return NotFound();

            entity.Sessionid = vm.Sessionid;
            entity.Traineeid = vm.Traineeid;
            entity.Value = vm.Value;

            _gradeRepo.Update(entity);

            TempData["Success"] = "Grade updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var grade = _gradeRepo.GetById(id);
            if (grade == null) return NotFound();
            return View(grade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _gradeRepo.Delete(id);
            TempData["Success"] = "Grade deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
