using WepApp.Models;

namespace WepApp.DataStore.Interfaces
{
    public interface IUserRepository
    {
        User? Login(string username, string password);
    }
}
