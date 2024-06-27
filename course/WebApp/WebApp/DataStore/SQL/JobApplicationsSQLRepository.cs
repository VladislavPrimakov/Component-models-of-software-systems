using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            var isAlreadyExist = db.JobApplications.Where(ja => ja.ResumeId == resume.ResumeId && ja.JobId == job.JobId).FirstOrDefault();
            if (isAlreadyExist != null) 
                return false;
            var jobApplication = new JobApplication { 
                JobApplicationStatus = JobApplicationStatus.NewFromEmployer, 
                ResumeId = resume.ResumeId,
                JobId = job.JobId,
                UpdatedAt = DateTime.Now.ToUniversalTime(),
            };
            db.JobApplications.Add(jobApplication);
            db.SaveChanges();
            return true;
        }

        public bool AddJobApplicationFromCandidate(int userId, int jobId)
        {
            var resume = db.Resumes.Where(r => r.UserId == userId).FirstOrDefault();
            if (resume == null)
                return false;
            var job = db.Jobs.Find(jobId);
            if (job == null)
                return false;
            var isAlreadyExist = db.JobApplications.Where(ja => ja.ResumeId == resume.ResumeId && ja.JobId == job.JobId).FirstOrDefault();
            if (isAlreadyExist != null)
                return false;
            var jobApplication = new JobApplication
            {
                JobApplicationStatus = JobApplicationStatus.NewFromCandidate,
                ResumeId = resume.ResumeId,
                JobId = job.JobId,
                UpdatedAt = DateTime.Now.ToUniversalTime(),
            };
            db.JobApplications.Add(jobApplication);
            db.SaveChanges();
            return true;
        }

        public bool AcceptJobApplication(int jobApplicationId)
        {
            return UpdateJobApplicationStatus(jobApplicationId, JobApplicationStatus.Accepted);
        }

        public bool RejectJobApplication(int jobApplicationId)
        {
            return UpdateJobApplicationStatus(jobApplicationId, JobApplicationStatus.Rejected);
        }

        private bool UpdateJobApplicationStatus(int jobApplicationId, JobApplicationStatus jobApplicationStatus) {
            var jobApplication = db.JobApplications.Find(jobApplicationId);
            if (jobApplication == null) 
                return false;
            jobApplication.JobApplicationStatus = jobApplicationStatus;
            jobApplication.UpdatedAt = DateTime.Now.ToUniversalTime();
            db.SaveChanges();
            return true;
        }

        public IEnumerable<JobApplication> GetAcceptedJobApplicationsWithResumeAndJobByUserId(int userId)
        {
            return GetJobApplicationsWithResumeAndJobByUserIdAndStatus(userId, "accepted");
        }

        public IEnumerable<JobApplication> GetNewJobApplicationsWithResumeAndJobByUserId(int userId)
        {
            return GetJobApplicationsWithResumeAndJobByUserIdAndStatus(userId, "new");
        }

        public IEnumerable<JobApplication> GetRejectedJobApplicationsWithResumeAndJobByUserId(int userId)
        {
            return GetJobApplicationsWithResumeAndJobByUserIdAndStatus(userId, "rejected");
        }

        public IEnumerable<JobApplication> GetInboxJobApplicationsWithResumeAndJobByUserId(int userId)
        {
            return GetJobApplicationsWithResumeAndJobByUserIdAndStatus(userId, "inbox");
        }

        private IEnumerable<JobApplication> GetJobApplicationsWithResumeAndJobByUserIdAndStatus(int userId, string status) {
            string role = db.Users.Include(u => u.Role)
                .Where(u => u.UserId == userId)
                .FirstOrDefault()!.Role!.Name;
            JobApplicationStatus _status = JobApplicationStatus.Accepted;
            if (role == "employer") {
                if (status == "new") _status = JobApplicationStatus.NewFromEmployer;
                if (status == "inbox") _status = JobApplicationStatus.NewFromCandidate;
            }
            if (role == "candidate") {
                if (status == "new") _status = JobApplicationStatus.NewFromCandidate;
                if (status == "inbox") _status = JobApplicationStatus.NewFromEmployer;
            }
            if (status == "accepted") _status = JobApplicationStatus.Accepted;
            if (status == "rejected") _status = JobApplicationStatus.Rejected;

            if (role == "candidate")
            {
                return db.JobApplications
                .Include(ja => ja.Resume).ThenInclude(r => r!.Category)
                .Include(ja => ja.Resume).ThenInclude(r => r!.Location)
                .Include(ja => ja.Job).ThenInclude(j => j!.Employer)
                .Include(ja => ja.Job).ThenInclude(j => j!.Category)
                .Include(ja => ja.Job).ThenInclude(j => j!.Location)
                .Where(ja => ja!.Resume!.UserId == userId && ja.JobApplicationStatus == _status)
                .ToList();
            }
            if (role == "employer")
            {
                return db.JobApplications
                .Include(ja => ja.Resume).ThenInclude(r => r!.Category)
                .Include(ja => ja.Resume).ThenInclude(r => r!.Location)
                .Include(ja => ja.Job).ThenInclude(j => j!.Employer)
                .Include(ja => ja.Job).ThenInclude(j => j!.Category)
                .Include(ja => ja.Job).ThenInclude(j => j!.Location)
                .Where(ja => ja!.Job!.Employer!.UserId == userId && ja.JobApplicationStatus == _status)
                .ToList();
            }
            return Enumerable.Empty<JobApplication>();
        }


    }
}
