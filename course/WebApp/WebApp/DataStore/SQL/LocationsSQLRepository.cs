using WebApp.DataStore.Interfaces;
using WebApp.Models;
using WepApp.DataStore.SQL;

namespace WebApp.DataStore.SQL
{
    public class LocationsSQLRepository : ILocationsRepository
    {
        private readonly ApplicationContext db;

        public LocationsSQLRepository(ApplicationContext db)
        {
            this.db = db;
        }
        public IEnumerable<Location> GetLocations()
        {
            return db.Locations
                .OrderBy(l => l.Name)
                .ToList();
        }
    }
}
