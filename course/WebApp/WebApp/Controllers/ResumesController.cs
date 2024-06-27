using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DataStore.Interfaces;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ResumesController : Controller
    {
        private readonly IResumesRepository resumeRepository;
        private readonly ICategoriesRepository categoriesRepository;
        private readonly ILocationsRepository locationsRepository;

        public ResumesController(IResumesRepository resumeRepository, ICategoriesRepository categoriesRepository, ILocationsRepository locationsRepository)
        {
            this.resumeRepository = resumeRepository;
            this.categoriesRepository = categoriesRepository;
            this.locationsRepository = locationsRepository;
        }

        [Authorize(Policy = "employers")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "candidates")]
        public IActionResult MyResume() {
            var resumeViewModel = new ResumeViewModel
            {
                Resume = resumeRepository.GetResumeByUserId(int.Parse(User.FindFirst("id")!.Value)),
                Categories = categoriesRepository.GetCategories().ToList(),
                Locations = locationsRepository.GetLocations().ToList()
            };
            return View(resumeViewModel);
        }

        [Authorize(Policy = "candidates")]
        [HttpPost]
        public IActionResult MyResume(ResumeViewModel resumeViewModel)
        {
            if (ModelState.IsValid) {
                resumeRepository.UpdateResume(resumeViewModel.Resume);
                return RedirectToAction(nameof(MyResume));
            }
            resumeViewModel.Categories = categoriesRepository.GetCategories().ToList();
            resumeViewModel.Locations = locationsRepository.GetLocations().ToList();
            return View(resumeViewModel);
        }
    }
}
