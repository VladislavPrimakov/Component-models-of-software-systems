using WebApp.Models;

namespace WebApp.DataStore.Interfaces
{
    public interface ILocationsRepository
    {
        public IEnumerable<Location> GetLocations();
    }
}
