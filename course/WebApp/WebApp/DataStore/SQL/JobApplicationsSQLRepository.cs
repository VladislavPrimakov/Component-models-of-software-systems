using Microsoft.EntityFrameworkCore;
using WebApp.DataStore.Interfaces;
using WebApp.Models;
using WepApp.DataStore.SQL;

namespace WebApp.DataStore.SQL
{
    public class JobApplicationsSQLRepository : IJobApplicationsRepository
    {
        private readonly ApplicationContext db;
        private enum StatusJobApplication { New, Accepted, Rejected, Inbox }

        public JobApplicationsSQLRepository(ApplicationContext db)
        {
            this.db = db;
        }
        public bool AddJobApplicationFromEmployer(int resumeId, int jobId)
        {
            var resume = db.Resumes.Find(resumeId);
            if (resume == null)
                return false;
            var job = db.Jobs.Find(jobId);
            if (job == null)
                return false;
            int statusId = db.JobApplicationStatuses.Where(s => s.Name == "new-from-employer").FirstOrDefault()!.JobApplicationStatusId;
            var jobApplication = new JobApplication { 
                JobApplicationStatusId = statusId, 
                ResumeId = resume.ResumeId,
                JobId = job.JobId,
                UpdatedAt = DateTime.Now.ToUniversalTime(),
            };
            db.JobApplications.Add(jobApplication);
            db.SaveChanges();
            return true;
        }

        public IEnumerable<JobApplication> GetAcceptedJobApplicationsWithResumeAndJobByUserId(int userId)
        {
            return GetJobApplicationsWithResumeAndJobByUserIdAndStatus(userId, StatusJobApplication.Accepted);
        }

        public IEnumerable<JobApplication> GetNewJobApplicationsWithResumeAndJobByUserId(int userId)
        {
            return GetJobApplicationsWithResumeAndJobByUserIdAndStatus(userId, StatusJobApplication.New);
        }

        public IEnumerable<JobApplication> GetRejectedJobApplicationsWithResumeAndJobByUserId(int userId)
        {
            return GetJobApplicationsWithResumeAndJobByUserIdAndStatus(userId, StatusJobApplication.Rejected);
        }

        public IEnumerable<JobApplication> GetInboxJobApplicationsWithResumeAndJobByUserId(int userId)
        {
            return GetJobApplicationsWithResumeAndJobByUserIdAndStatus(userId, StatusJobApplication.Inbox);
        }

        private IEnumerable<JobApplication> GetJobApplicationsWithResumeAndJobByUserIdAndStatus(int userId, StatusJobApplication status) {
            string role = db.Users.Include(u => u.Role)
                .Where(u => u.UserId == userId)
                .FirstOrDefault()!.Role!.Name;
            string statusString = "";
            if (role == "employer") {
                if (status == StatusJobApplication.New) statusString = "new-from-employer";
                if (status == StatusJobApplication.Accepted) statusString = "accepted-from-employer";
                if (status == StatusJobApplication.Rejected) statusString = "rejected-from-employer";
                if (status == StatusJobApplication.Inbox) statusString = "new-from-user";
            }
            if (role == "candidate") {
                if (status == StatusJobApplication.New) statusString = "new-from-user";
                if (status == StatusJobApplication.Accepted) statusString = "accepted-from-user";
                if (status == StatusJobApplication.Rejected) statusString = "rejected-from-user";
                if (status == StatusJobApplication.Inbox) statusString = "new-from-employer";
            }
            var statusId = db.JobApplicationStatuses.Where(s => s.Name == statusString).FirstOrDefault()?.JobApplicationStatusId;
            if (statusId == null)
                return Enumerable.Empty<JobApplication>();
            if (role == "candidate")
            {
                return db.JobApplications
                .Include(ja => ja.Resume)
                .Include(ja => ja.Job)
                .Where(ja => ja!.Resume!.UserId == userId && ja.JobApplicationStatusId == statusId)
                .ToList();
            }
            if (role == "employer")
            {
                return db.JobApplications
                .Include(ja => ja.Resume)
                .Include(ja => ja.Job)
                .ThenInclude(j => j.Employer)
                .Where(ja => ja!.Job!.Employer!.UserId == userId && ja.JobApplicationStatusId == statusId)
                .ToList();
            }
            return Enumerable.Empty<JobApplication>();
        }
    }
}
