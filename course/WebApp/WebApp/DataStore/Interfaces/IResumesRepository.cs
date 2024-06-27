using WebApp.Models;

namespace WebApp.DataStore.Interfaces
{
    public interface IResumesRepository
    {
        Resume? GetResumeByUserId(int userId);
        Resume? GetResumeWithCategoryAndLocationById(int id);
        void UpdateResume(Resume resume);
        List<Resume> GetActiveResumesWithCategoryAndLocation(int? categoryId, int? locationId, int? minExperience);
    }
}
