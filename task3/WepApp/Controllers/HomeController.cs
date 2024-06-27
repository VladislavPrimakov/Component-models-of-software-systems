using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WepApp.DataStore.Interfaces;
using WepApp.Models;
using WepApp.ViewModels;

namespace WepApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository userRepository;

        public HomeController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel userViewModel) {
            if (ModelState.IsValid) {
                var user = userRepository.Login(userViewModel.UserName, userViewModel.Password);
                if (user == null) {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View(userViewModel);
                }
                var claims = new List<Claim> {
                    new Claim("name", user.UserName),
                    new Claim("role", user?.Role?.Name)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect("/Products/Index");
            }
                return View(userViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout() { 
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
