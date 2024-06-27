using WepApp.Models;

namespace WepApp.DataStore.Interfaces
{
    public interface IUsersRepository
    {
        User? Login(string email, string password);
        bool Register(User user);
    }
}
