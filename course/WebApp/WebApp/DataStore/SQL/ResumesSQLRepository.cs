using Microsoft.EntityFrameworkCore;
using WebApp.DataStore.Interfaces;
using WebApp.Models;
using WepApp.DataStore.SQL;

namespace WebApp.DataStore.SQL
{
    public class ResumesSQLRepository : IResumesRepository
    {
        private readonly ApplicationContext db;

        public ResumesSQLRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public List<Resume> GetActiveResumesWithCategoryAndLocation(int? categoryId, int? locationId, int? minExperience)
        {
            IQueryable<Resume> resumes = db.Resumes;
            resumes = resumes.Where(r => r.IsActive == true);
            if (categoryId != null && categoryId != 0)
                resumes = resumes.Where(r => r.CategoryId == categoryId);
            if (locationId != null && locationId != 0)
                resumes = resumes.Where(r => r.LocationId == locationId);
            if (minExperience != null)
                resumes = resumes.Where(r => r.Experience >= minExperience);
            resumes = resumes.OrderBy(r => r.Experience).ThenBy(r => r.UpdatedAt);
            resumes = resumes.Include(r => r.Category).Include(r => r.Location);
            return resumes.ToList();
        }

        public Resume? GetResumeByUserId(int userId)
        {
            return db.Resumes.Where(r => r.UserId == userId).FirstOrDefault();
        }

        public Resume? GetResumeWithCategoryAndLocationById(int id)
        {
            return db.Resumes
                .Include(r => r.Category)
                .Include(r => r.Location)
                .Where(r => r.ResumeId == id)
                .FirstOrDefault();
        }

        public void UpdateResume(Resume resume)
        {
            var _resume = db.Resumes.Find(resume.ResumeId);
            if (_resume != null)
            {
                _resume.Title = resume.Title;
                _resume.Description = resume.Description;
                _resume.Experience = resume.Experience;
                _resume.Education = resume.Education;
                _resume.CategoryId = resume.CategoryId;
                _resume.LocationId = resume.LocationId;
                _resume.IsActive = resume.IsActive;
                var now = DateTime.Now;
                _resume.UpdatedAt = new DateOnly(now.Year, now.Month, now.Day);
                db.Resumes.Update(_resume);
                db.SaveChanges();
            }
        }
    }
}
