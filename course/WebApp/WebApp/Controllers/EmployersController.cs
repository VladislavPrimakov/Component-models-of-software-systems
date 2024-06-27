using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.DataStore.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EmployersController : Controller
    {
        private readonly IEmployersRepository employersRepository;

        public EmployersController(IEmployersRepository employersRepository)
        {
            this.employersRepository = employersRepository;
        }

        [Authorize(Policy = "employers")]
        public IActionResult MyInfo()
        {
            return View(employersRepository.GetEmmployerByUserId(int.Parse(User.FindFirstValue("id") ?? "0")));
        }

        [Authorize(Policy = "employers")]
        [HttpPost]
        public IActionResult MyInfo(Employer employer)
        {
            if (ModelState.IsValid) { 
                employersRepository.UpdateEmployer(employer);
                return RedirectToAction(nameof(MyInfo));
            }
            return View(employer);
        }
    }
}
