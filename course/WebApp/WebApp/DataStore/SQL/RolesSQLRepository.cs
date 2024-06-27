using WebApp.DataStore.Interfaces;
using WepApp.DataStore.SQL;
using WepApp.Models;

namespace WebApp.DataStore.SQL
{
    public class RolesSQLRepository : IRolesRepository
    {
        private readonly ApplicationContext db;

        public RolesSQLRepository(ApplicationContext db) {
            this.db = db;
        }

        public IEnumerable<Role> GetRoles()
        {
            return db.Roles.ToList();
        }
    }
}
