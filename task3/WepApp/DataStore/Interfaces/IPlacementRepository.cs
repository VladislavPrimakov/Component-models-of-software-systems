using WepApp.Models;

namespace WepApp.DataStore.Interfaces
{
    public interface IPlacementRepository
    {
        IEnumerable<Placement> GetPlacements();
    }
}
