using WebApp.Models;

namespace WebApp.ViewModels
{
    public class JobApplicationsViewModel
    {
        public List<JobApplication> NewJobApplications { get; set; } = new List<JobApplication>();
        public List<JobApplication> AcceptedJobApplications { get; set; } = new List<JobApplication>();
        public List<JobApplication> RejectedJobApplications { get; set; } = new List<JobApplication>();
        public List<JobApplication> InboxJobApplications { get; set; } = new List<JobApplication>();
    }
}
