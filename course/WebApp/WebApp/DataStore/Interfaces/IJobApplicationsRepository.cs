using WebApp.Models;

namespace WebApp.DataStore.Interfaces
{
    public interface IJobApplicationsRepository
    {
        bool AddJobApplicationFromEmployer(int resumeId, int jobId);
        bool AddJobApplicationFromCandidate(int userId, int jobId);

        bool AcceptJobApplication(int jobApplicationId);
        bool RejectJobApplication(int jobApplicationId);

        IEnumerable<JobApplication> GetInboxJobApplicationsWithResumeAndJobByUserId(int userId);
        IEnumerable<JobApplication> GetNewJobApplicationsWithResumeAndJobByUserId(int userId);
        IEnumerable<JobApplication> GetAcceptedJobApplicationsWithResumeAndJobByUserId(int userId);
        IEnumerable<JobApplication> GetRejectedJobApplicationsWithResumeAndJobByUserId(int userId);


    }
}
