using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DataStore.Interfaces;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobsRepository jobsRepository;
        private readonly ILocationsRepository locationsRepository;
        private readonly ICategoriesRepository categoriesRepository;

        public JobsController(IJobsRepository jobsRepository, ILocationsRepository locationsRepository, ICategoriesRepository categoriesRepository)
        {
            this.jobsRepository = jobsRepository;
            this.locationsRepository = locationsRepository;
            this.categoriesRepository = categoriesRepository;
        }

        [Authorize(Policy = "candidates")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "employers")]
        public IActionResult MyJobs() {
            var jobs = jobsRepository.GetAllJobsWithCaregoryAndLocation(int.Parse(User.FindFirst("id")!.Value));
            return View(jobs); 
        }

        [Authorize(Policy = "employers")]
        public IActionResult Add() {
            var jobViewModel = new JobViewModel();
            jobViewModel.Categories = categoriesRepository.GetCategories().ToList();
            jobViewModel.Locations = locationsRepository.GetLocations().ToList();
            ViewBag.Action = "Add";
            return View(jobViewModel);
        }

        [Authorize(Policy = "employers")]
        [HttpPost]
        public IActionResult Add(JobViewModel jobViewModel)
        {
            if (ModelState.IsValid) {
                jobsRepository.AddJob(int.Parse(User.FindFirst("id")!.Value), jobViewModel.Job);
                return Redirect(nameof(MyJobs));
            }
            jobViewModel.Categories = categoriesRepository.GetCategories().ToList();
            jobViewModel.Locations = locationsRepository.GetLocations().ToList();
            ViewBag.Action = "Add";
            return View(jobViewModel);
        }

        [Authorize(Policy = "employers")]
        public IActionResult Edit(int? id)
        {
            var jobViewModel = new JobViewModel();
            jobViewModel.Categories = categoriesRepository.GetCategories().ToList();
            jobViewModel.Locations = locationsRepository.GetLocations().ToList();
            ViewBag.Action = "Edit";
            return View(jobViewModel);
        }

        [Authorize(Policy = "employers")]
        [HttpPost]
        public IActionResult Edit(JobViewModel jobViewModel)
        {
            if (ModelState.IsValid)
            {
                jobsRepository.AddJob(int.Parse(User.FindFirst("id")!.Value), jobViewModel.Job);
                return Redirect(nameof(MyJobs));
            }
            ViewBag.Action = "Edit";
            jobViewModel.Categories = categoriesRepository.GetCategories().ToList();
            jobViewModel.Locations = locationsRepository.GetLocations().ToList();
            return View(jobViewModel);
        }

        [Authorize(Policy = "employers")]
        public IActionResult Delete(int? id) {
            if (id != null) {
            jobsRepository.DeleteJob(id);
        }
            return RedirectToAction(nameof(MyJobs));
        
        }
    }
}
