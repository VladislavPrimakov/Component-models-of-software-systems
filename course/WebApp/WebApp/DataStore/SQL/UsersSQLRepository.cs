using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WepApp.DataStore.Interfaces;
using WepApp.Models;

namespace WepApp.DataStore.SQL
{
    public class UsersSQLRepository : IUsersRepository
    {
        private readonly ApplicationContext db;

        public UsersSQLRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public bool CanManageJob(int userId, int jobId)
        {
            var employer = db.Employers.Where(e  => e.UserId == userId).FirstOrDefault();
            if (employer == null) return false;
            var job = db.Jobs.Where(j => j.JobId == jobId && j.EmployerId == employer.EmployerId).FirstOrDefault();
            if (job == null) return false;
            return true;
        }

        public User? Login(string email, string password)
        {
            return db.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == email && u.Password == password);
        }

         public bool Register(User user)
        {
            var _user = db.Users
                .Where(u => u.Email == user.Email)
                .FirstOrDefault();
            if (_user == null)
            {
                db.Users.Add(user);
                db.SaveChanges();

                var role = db.Roles.Find(user.RoleId);
                if (role?.Name == "candidate")
                {
                    var now = DateTime.Now;
                    var resume = new Resume { Description = "", Title = "", UserId = user.UserId, UpdatedAt = new DateOnly(now.Year, now.Month, now.Day) };
                    db.Resumes.Add(resume);
                    db.SaveChanges();
                }
                if (role?.Name == "employer")
                {
                    var employer = new Employer{ CompanyName = "", CompanyDescription = "", Contacts = "", UserId = user.UserId };
                    db.Employers.Add(employer);
                    db.SaveChanges();
                }
                return true;
            }
            return false;
        }
    }
}
