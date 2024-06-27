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
        private readonly IJobsRepository jobsRepository;

        public ResumesController(IResumesRepository resumeRepository, ICategoriesRepository categoriesRepository, ILocationsRepository locationsRepository, IJobsRepository jobsRepository)
        {
            this.resumeRepository = resumeRepository;
            this.categoriesRepository = categoriesRepository;
            this.locationsRepository = locationsRepository;
            this.jobsRepository = jobsRepository;
        }

        [Authorize(Policy = "employers")]
        public IActionResult Index(ResumesViewModel resumesViewModel)
        {
            if (ModelState.IsValid)
            {
                resumesViewModel.Resumes = resumeRepository.GetActiveResumesWithCategoryAndLocation(resumesViewModel.CategoryId, resumesViewModel.LocationId, resumesViewModel.MinExperience);
            }
            resumesViewModel.Locations = locationsRepository.GetLocations().ToList();
            resumesViewModel.Categories = categoriesRepository.GetCategories().ToList();
            return View(resumesViewModel);
        }

        [Authorize(Policy = "employers")]
        public IActionResult View(int id) {
            var resume = resumeRepository.GetResumeWithCategoryAndLocationById(id);
            if (resume != null)
            {
                var viewResumeViewModel = new ViewResumeViewModel();
                viewResumeViewModel.Resume = resume;
                viewResumeViewModel.Jobs = jobsRepository.GetAllJobsWithCaregoryAndLocation(int.Parse(User.FindFirst("id")!.Value)).ToList();
                return View(viewResumeViewModel);
            }
            return NotFound();
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
