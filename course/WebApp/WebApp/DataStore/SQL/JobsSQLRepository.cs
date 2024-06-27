using Microsoft.EntityFrameworkCore;
using WebApp.DataStore.Interfaces;
using WebApp.Models;
using WepApp.DataStore.SQL;

namespace WebApp.DataStore.SQL
{
    public class JobsSQLRepository : IJobsRepository
    {
        private readonly ApplicationContext db;

        public JobsSQLRepository(ApplicationContext db)
        {
            this.db = db;
        }
        public void AddJob(int userId, Job job)
        {
            int employerId = db.Employers.Where(e => e.UserId == userId).Select(e => e.EmployerId).FirstOrDefault();
            job.EmployerId = employerId;
            var now = DateTime.Now;
            job.PostedAt = new DateOnly(now.Year, now.Month, now.Day);
            db.Jobs.Add(job);
            db.SaveChanges();
        }

        public void DeleteJob(int jobId)
        {
            var job = db.Jobs.Find(jobId);
            if (job != null)
            {
                db.Jobs.Remove(job);
                db.SaveChanges();
            }
        }

        public List<Job> GetAllJobsWithCaregoryAndLocation(int userId)
        {
            int employerId = db.Employers.Where(e => e.UserId == userId).Select(e => e.EmployerId).FirstOrDefault();
            return db.Jobs
                .Include(j => j.Category)
                .Include(j => j.Location)
                .Where(j => j.EmployerId ==  employerId)
                .OrderBy(j => j.PostedAt)
                .ToList();
        }

        public Job? GetJobById(int jobId)
        {
            return db.Jobs.Find(jobId);
        }

        public void UpdateJob(Job job)
        {
            var _job = db.Jobs.Find(job.JobId);
            if (_job != null)
            {
                _job.Title = job.Title;
                _job.Description = job.Description;
                _job.Salary = job.Salary;
                _job.MinExpirience = job.MinExpirience;
                _job.IsActive = job.IsActive;
                _job.LocationId = job.LocationId;
                _job.CategoryId = job.CategoryId;
                db.Jobs.Update(_job);
                db.SaveChanges();
            }
        }
    }
}
