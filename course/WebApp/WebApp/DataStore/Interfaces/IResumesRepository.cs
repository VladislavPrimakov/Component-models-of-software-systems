using WebApp.Models;

namespace WebApp.DataStore.Interfaces
{
    public interface IResumesRepository
    {
        Resume? GetResumeByUserId(int userId);
        void UpdateResume(Resume resume);
    }
}
