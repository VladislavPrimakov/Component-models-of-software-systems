using WebApp.DataStore.Interfaces;
using WebApp.Models;
using WepApp.DataStore.SQL;

namespace WebApp.DataStore.SQL
{
    public class EmployersSQLRepository : IEmployersRepository
    {
        private readonly ApplicationContext db;

        public EmployersSQLRepository(ApplicationContext db)
        {
            this.db = db;
        }
        public Employer? GetEmmployerByUserId(int userId)
        {
            return db.Employers.Where(r => r.UserId == userId).FirstOrDefault();

        }

        public void UpdateEmployer(Employer employer)
        {
            var _employer = db.Employers.Find(employer.EmployerId);
            if (_employer != null)
            {
                _employer.CompanyDescription = employer.CompanyDescription;
                _employer.CompanyName = employer.CompanyName;
                _employer.Contacts = employer.Contacts;

                db.Employers.Update(_employer);
                db.SaveChanges();
            }
        }
    }
}
