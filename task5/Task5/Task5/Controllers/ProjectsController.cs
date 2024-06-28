using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task5.DataStore;
using Task5.Functions;
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
            var projectViewModel = new ProjectViewModel();
            projectViewModel.Projects = projectRepository.GetProjects();
            return View(projectViewModel);
        }

        [HttpPost]
        public IActionResult Index(ProjectViewModel projectViewModel)
        {
            projectViewModel.Projects = projectRepository.GetProjects();
            if (ModelState.IsValid && projectViewModel.InputDataProjectId != null) {
                projectViewModel.Result = MonteCarlo.Run(
                    projectViewModel.Projects.Where(p => p.ProjectId == projectViewModel.InputDataProjectId).FirstOrDefault()!,
                    projectViewModel.Simulations,
                    projectViewModel.Confidence
                    );
            }
            return View(projectViewModel);
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
