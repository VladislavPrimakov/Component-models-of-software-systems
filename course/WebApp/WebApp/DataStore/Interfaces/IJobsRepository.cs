using WebApp.Models;

namespace WebApp.DataStore.Interfaces
{
    public interface IJobsRepository
    {
        void AddJob(int userId, Job job);
        void UpdateJob(Job job);
        void DeleteJob(int jobId);
        List<Job> GetAllJobsWithCaregoryAndLocation(int userId);
        Job? GetJobById(int jobId);
    }
}
