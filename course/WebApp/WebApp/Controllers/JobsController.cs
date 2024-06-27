using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DataStore.Interfaces;

namespace WebApp.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobsRepository jobsRepository;

        public JobsController(IJobsRepository jobsRepository)
        {
            this.jobsRepository = jobsRepository;
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
    }
}
