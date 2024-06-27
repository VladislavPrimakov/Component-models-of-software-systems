using WebApp.DataStore.Interfaces;
using WebApp.Models;
using WepApp.DataStore.SQL;

namespace WebApp.DataStore.SQL
{
    public class CategoriesSQLRepository : ICategoriesRepository
    {
        private readonly ApplicationContext db;

        public CategoriesSQLRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public IEnumerable<Category> GetCategories()
        {
            return db.Categories
                .OrderBy(c => c.Name)
                .ToList();
        }
    }
}
