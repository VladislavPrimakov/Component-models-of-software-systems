using Microsoft.EntityFrameworkCore;
using Task5.Models;
using WepApp.DataStore.SQL;

namespace Task5.DataStore
{
    public class ProjectRepository
    {
        private readonly ApplicationContext db;

        public ProjectRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public void AddProject(Project project) {
            db.Projects.Add(project);
            db.SaveChanges();
        }

        public void DeleteProject(int projectId)
        {
            var proj = db.Projects.Find(projectId);
            if (proj == null) return;
            db.Projects.Remove(proj);
            db.SaveChanges();
        }

        public List<Project> GetProjects() {
            return db.Projects.Include(p => p.Risks).ToList();
        }
    }
}
