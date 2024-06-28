using Task5.Models;
using WepApp.DataStore.SQL;

namespace Task5.DataStore
{
    public class RisksRepository
    {
        private readonly ApplicationContext db;

        public RisksRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public void AddRisk(Risk risk) { 
            db.Risks.Add(risk);
            db.SaveChanges();
        }

        public void DeleteRisk(int riskId)
        {
            var risk = db.Risks.Find(riskId);
            if (risk == null) return;
            db.Risks.Remove(risk);
            db.SaveChanges();
        }
    }
}
