using WebApp.Models;

namespace WebApp.DataStore.Interfaces
{
    public interface IJobApplicationsRepository
    {
        bool AddJobApplicationFromEmployer(int userId, int jobId);
        IEnumerable<JobApplication> GetNewJobApplicationsWithResumeAndJobByUserId(int userId);
        IEnumerable<JobApplication> GetAcceptedJobApplicationsWithResumeAndJobByUserId(int userId);
        IEnumerable<JobApplication> GetRejectedJobApplicationsWithResumeAndJobByUserId(int userId);
        IEnumerable<JobApplication> GetInboxJobApplicationsWithResumeAndJobByUserId(int userId);


    }
}
