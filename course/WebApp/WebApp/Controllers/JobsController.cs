using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        public IActionResult Index(JobsViewModel jobsViewModel)
        {
            if (ModelState.IsValid) {
                jobsViewModel.Jobs = jobsRepository.GetActiveJobsWithCaregoryAndLocationAndEmployer(jobsViewModel.CategoryId, jobsViewModel.LocationId, jobsViewModel.MinSalary, jobsViewModel.MinExperience);
            }
            jobsViewModel.Locations = locationsRepository.GetLocations().ToList();
            jobsViewModel.Categories = categoriesRepository.GetCategories().ToList();
            return View(jobsViewModel);
        }

        [Authorize]
        public IActionResult View(int id)
        {
            var job = jobsRepository.GetJobWithEmployerAndLocationAndCatergoryById(id);
            if (job != null)
                return View(job);
            return NotFound();
        }

        [Authorize(Policy = "employers")]
        public IActionResult MyJobs() {
            var jobs = jobsRepository.GetAllJobsWithCaregoryAndLocationByUserId(int.Parse(User.FindFirstValue("id") ?? ""));
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
                jobsRepository.AddJob(int.Parse(User.FindFirstValue("id") ?? "0"), jobViewModel.Job);
                return Redirect(nameof(MyJobs));
            }
            jobViewModel.Categories = categoriesRepository.GetCategories().ToList();
            jobViewModel.Locations = locationsRepository.GetLocations().ToList();
            ViewBag.Action = "Add";
            return View(jobViewModel);
        }

        [Authorize(Policy = "employers")]
        public IActionResult Edit(int id)
        {
            var job = jobsRepository.GetJobById(id);
            if (job != null)
            {
                var jobViewModel = new JobViewModel();
                jobViewModel.Job = job;
                jobViewModel.Categories = categoriesRepository.GetCategories().ToList();
                jobViewModel.Locations = locationsRepository.GetLocations().ToList();
                ViewBag.Action = "Edit";
                return View(jobViewModel);
            }
            return NotFound();
        }

        [Authorize(Policy = "employers")]
        [HttpPost]
        public IActionResult Edit(JobViewModel jobViewModel)
        {
            if (ModelState.IsValid)
            {
                jobsRepository.UpdateJob(jobViewModel.Job);
                return RedirectToAction(nameof(MyJobs));
            }
            ViewBag.Action = "Edit";
            jobViewModel.Categories = categoriesRepository.GetCategories().ToList();
            jobViewModel.Locations = locationsRepository.GetLocations().ToList();
            return View(jobViewModel);
        }

        [Authorize(Policy = "employers")]
        public IActionResult Delete(int id) {
            jobsRepository.DeleteJob(id);
            return RedirectToAction(nameof(MyJobs));
        
        }
    }
}
