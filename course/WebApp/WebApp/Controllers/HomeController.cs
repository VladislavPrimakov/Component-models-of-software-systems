using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Security.Claims;
using WebApp.DataStore.Interfaces;
using WebApp.ViewModels;
using WepApp.DataStore.Interfaces;
using WepApp.Models;
using WepApp.ViewModels;

namespace WepApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersRepository userRepository;
        private readonly IRolesRepository roleRepository;
        private readonly IResumesRepository resumeRepository;

        public HomeController(IUsersRepository userRepository, IRolesRepository roleRepository, IResumesRepository resumeRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.resumeRepository = resumeRepository;
        }

        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel userViewModel) {
            if (ModelState.IsValid) {
                var user = userRepository.Login(userViewModel.Email, userViewModel.Password);
                if (user == null) {
                    ModelState.AddModelError("", "Incorrect email or password");
                    return View(userViewModel);
                }
                var claims = new List<Claim> {
                    new Claim("name", user.Email),
                    new Claim("role", user!.Role!.Name),
                    new Claim("id", user.UserId.ToString())
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                if (user?.Role?.Name == "employer") {
                    return Redirect("/Resumes/Index");
                }
                if (user?.Role?.Name == "candidate") {
                    return Redirect("/Jobs/Index");
                }
            }
                return View(userViewModel);
        }

        public IActionResult Register() {
            var addUserViewModel = new AddUserViewModel();
            addUserViewModel.Roles = roleRepository.GetRoles().ToList();
            return View(addUserViewModel);
        }

        [HttpPost]
        public IActionResult Register(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.Register(addUserViewModel.User);
                if (user == true)
                {
                    return RedirectToAction(nameof(Login));
                }
                else {
                    ModelState.AddModelError("Email", "This email has already exist");
                }
            }
            addUserViewModel.Roles = roleRepository.GetRoles().ToList();
            return View(addUserViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout() { 
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
