using WebApp.Models;

namespace WebApp.DataStore.Interfaces
{
    public interface IJobsRepository
    {
        void AddJob(int userId, Job job);
        void UpdateJob(Job job);
        void DeleteJob(int jobId);
        List<Job> GetAllJobsWithCaregoryAndLocation(int userId);
        List<Job> GetActiveJobsWithCaregoryAndLocationAndEmployer(int? idCategory, int? idLocation, int? minSalary, int? minExpirience);
        Job? GetJobById(int jobId);
        Job? GetJobWithEmployerAndLocationAndCatergoryById(int jobId);
    }
}
