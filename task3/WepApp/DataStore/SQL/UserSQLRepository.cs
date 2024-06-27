using Microsoft.EntityFrameworkCore;
using WepApp.DataStore.Interfaces;
using WepApp.Models;

namespace WepApp.DataStore.SQL
{
    public class UserSQLRepository : IUserRepository
    {
        private readonly ApplicationContext db;

        public UserSQLRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public User? Login(string username, string password)
        {
            return db.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.UserName == username && u.Password == password);
        }
    }
}
