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


        public Resume? GetResumeByUserId(int userId)
        {
            return db.Resumes.Where(r => r.UserId == userId).FirstOrDefault();
        }

        public void UpdateResume(Resume resume)
        {
            var _resume = db.Resumes.Find(resume.ResumeId);
            if (_resume != null)
            {
                _resume.Title = resume.Title;
                _resume.Description = resume.Description;
                _resume.Expirience = resume.Expirience;
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
