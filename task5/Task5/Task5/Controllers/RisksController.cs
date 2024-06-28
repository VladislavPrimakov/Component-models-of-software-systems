using Microsoft.AspNetCore.Mvc;
using Task5.DataStore;
using Task5.Models;

namespace Task5.Controllers
{
    public class RisksController : Controller
    {
        private readonly RisksRepository risksRepository;
        private readonly ProjectRepository projectRepository;

        public RisksController(RisksRepository risksRepository, ProjectRepository projectRepository)
        {
            this.risksRepository = risksRepository;
            this.projectRepository = projectRepository;
        }
 
        public IActionResult Add()
        {
            var riskViewModel = new RiskViewModel();
            riskViewModel.Projects = projectRepository.GetProjects();
            return View(riskViewModel);
        }

        [HttpPost]
        public IActionResult Add(RiskViewModel riskViewModel)
        {
            if (ModelState.IsValid)
            {
                risksRepository.AddRisk(riskViewModel.Risk);
                return RedirectToAction("Index", "Projects");
            }
            riskViewModel.Projects = projectRepository.GetProjects();
            return View(riskViewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {   risksRepository.DeleteRisk(id);
            return RedirectToAction("Index", "Projects");
        }
    }
}
