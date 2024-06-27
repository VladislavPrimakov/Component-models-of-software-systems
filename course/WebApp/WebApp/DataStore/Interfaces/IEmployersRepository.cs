using WebApp.Models;

namespace WebApp.DataStore.Interfaces
{
    public interface IEmployersRepository
    {
        Employer? GetEmmployerByUserId(int userId);
        void UpdateEmployer(Employer employer);
    }
}
