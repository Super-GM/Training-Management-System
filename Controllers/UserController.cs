using Microsoft.AspNetCore.Mvc;
using Training_Management_System.Models;
using Training_Management_System.Repositories.Implementation;
using Training_Management_System.ViewModels;
namespace Training_Management_System.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository UserRepository;
        public UserController(UserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }
        public IActionResult Index()
        {
            try
            {
                List<User> AllUser = UserRepository.GetAll().ToList();
                List<UserViewModel> AllUserViewModels = AllUser.Select(U => new UserViewModel(U)).ToList();
                return View(AllUserViewModels);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult SaveCreate(UserViewModel UserFromClient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserRepository.Add(new Models.User(UserFromClient));
                    return RedirectToAction(nameof(Index));
                }
                return View(nameof(Create), UserFromClient);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        public IActionResult Edit(int Id)
        {
            try
            {
                User UserThatClientWantToEdit = UserRepository.GetById(Id)!;
                if (UserThatClientWantToEdit is null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(UserThatClientWantToEdit);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult Edit(User UserTHatWantToUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(UserTHatWantToUpdate);
                }
                UserRepository.Update(UserTHatWantToUpdate);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                User UserThatClientWantToDelete = UserRepository.GetById(Id)!;
                if (UserThatClientWantToDelete is null)
                {
                    return RedirectToAction(nameof(Index));
                }
                UserRepository.Delete(Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

    }
}
