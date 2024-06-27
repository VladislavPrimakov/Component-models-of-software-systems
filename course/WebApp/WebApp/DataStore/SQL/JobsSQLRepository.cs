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

        public List<Job> GetActiveJobsWithCaregoryAndLocationAndEmployer(int? categoryId, int? locationID, int? minSalary, int? minExperience)
        {
            IQueryable<Job> jobs = db.Jobs;
            jobs = jobs.Where(j => j.IsActive == true);
            if (categoryId != null && categoryId != 0)
                jobs = jobs.Where(j => j.CategoryId == categoryId);
            if (locationID != null && locationID != 0)
                jobs = jobs.Where(j => j.LocationId == locationID);
            if (minSalary != null)
                jobs = jobs.Where(j => j.Salary >= minSalary);
            if (minExperience != null)
                jobs = jobs.Where(j => j.MinExperience >= minExperience);
            jobs = jobs.OrderBy(j => j.Salary).ThenBy(j => j.MinExperience).ThenBy(j => j.PostedAt);
            jobs = jobs.Include(j => j.Category).Include(j => j.Location).Include(j => j.Employer);
            return jobs.ToList();
        }

        public List<Job> GetAllActiveJobsByUserId(int userId)
        {
            int employerId = db.Employers.Where(e => e.UserId == userId).Select(e => e.EmployerId).FirstOrDefault();
            return db.Jobs
                .Where(j => j.IsActive == true && j.EmployerId == employerId)
                .OrderBy(j => j.PostedAt)
                .ToList();
        }

        public List<Job> GetAllJobsWithCaregoryAndLocationByUserId(int userId)
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

        public Job? GetJobWithEmployerAndLocationAndCatergoryById(int jobId)
        {
            return db.Jobs
                .Include(j => j.Category)
                .Include(j => j.Location)
                .Include(j => j.Employer)
                .Where(j => j.JobId == jobId)
                .FirstOrDefault();
        }

        public void UpdateJob(Job job)
        {
            var _job = db.Jobs.Find(job.JobId);
            if (_job != null)
            {
                _job.Title = job.Title;
                _job.Description = job.Description;
                _job.Salary = job.Salary;
                _job.MinExperience = job.MinExperience;
                _job.IsActive = job.IsActive;
                _job.LocationId = job.LocationId;
                _job.CategoryId = job.CategoryId;
                db.Jobs.Update(_job);
                db.SaveChanges();
            }
        }
    }
}
