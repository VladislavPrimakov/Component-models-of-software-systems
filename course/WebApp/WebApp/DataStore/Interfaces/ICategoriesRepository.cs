using WebApp.Models;

namespace WebApp.DataStore.Interfaces
{
    public interface ICategoriesRepository
    {
        public IEnumerable<Category> GetCategories();
    }
}
