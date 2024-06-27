using WepApp.Models;

namespace WebApp.DataStore.Interfaces
{
    public interface IRolesRepository
    {
        IEnumerable<Role> GetRoles();
    }
}
