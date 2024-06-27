using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.DataStore.Interfaces;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class JobApplicationsController : Controller
    {
        private readonly IJobApplicationsRepository jobApplicationsRepository;

        public JobApplicationsController(IJobApplicationsRepository jobApplicationsRepository)
        {
            this.jobApplicationsRepository = jobApplicationsRepository;
        }

        [Authorize]
        public IActionResult MyJobApplications() {
            var jobApplicationsViewModel = new JobApplicationsViewModel();
            int userId = int.Parse(User.FindFirst("id")!.Value);
            jobApplicationsViewModel.NewJobApplications = jobApplicationsRepository.GetNewJobApplicationsWithResumeAndJobByUserId(userId).ToList();
            jobApplicationsViewModel.AcceptedJobApplications = jobApplicationsRepository.GetAcceptedJobApplicationsWithResumeAndJobByUserId(userId).ToList();
            jobApplicationsViewModel.RejectedJobApplications = jobApplicationsRepository.GetRejectedJobApplicationsWithResumeAndJobByUserId(userId).ToList();
            jobApplicationsViewModel.InboxJobApplications = jobApplicationsRepository.GetInboxJobApplicationsWithResumeAndJobByUserId(userId).ToList();

            return View(jobApplicationsViewModel);
        }

        [Authorize(Policy = "employers")]
        [HttpPost]
        public IActionResult AddFromEmployer(int jobId, int resumeId)
        {
            var isAdded = jobApplicationsRepository.AddJobApplicationFromEmployer(resumeId, jobId);
            if (isAdded)
                return RedirectToAction(nameof(MyJobApplications));
            return NotFound();
        }
    }
}
