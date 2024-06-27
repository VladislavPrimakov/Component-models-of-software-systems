using WepApp.DataStore.Interfaces;
using WepApp.Models;

namespace WepApp.DataStore.SQL
{
    public class PlacementSQLRepository : IPlacementRepository
    {
        private readonly ApplicationContext db;

        public PlacementSQLRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public IEnumerable<Placement> GetPlacements()
        {
            return db.Placements.ToList();
        }
    }
}
