using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task5.DataStore;
using Task5.Models;

namespace Task5.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ProjectRepository projectRepository;

        public ProjectsController(ProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public IActionResult Index()
        {
            return View(projectRepository.GetProjects());
        }

        public IActionResult Add() {
            var proj = new Project();
            return View(proj);
        }

        [HttpPost]
        public IActionResult Add(Project project)
        {
            if (ModelState.IsValid) { 
                projectRepository.AddProject(project);
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        [HttpPost]
        public IActionResult Delete(int id) {
            projectRepository.DeleteProject(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
