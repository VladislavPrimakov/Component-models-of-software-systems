using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WepApp.DataStore.Interfaces;
using WepApp.Models;
using WepApp.ViewModels;

namespace WepApp.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IPlacementRepository placementRepository;

        public ProductsController(IProductRepository productRepository, IPlacementRepository placementRepository)
        {
            this.productRepository = productRepository;
            this.placementRepository = placementRepository;
        }

        public IActionResult Index(ProductsViewModel productsViewModel) {
            productsViewModel.Products = productRepository.GetProducts(productsViewModel.ExpiredDate, productsViewModel.PlacementId, productsViewModel.InStock).ToList();
            productsViewModel.Placements = placementRepository.GetPlacements().ToList();
            return View(productsViewModel);
        }

        [Authorize(Policy = "admins")]
        public IActionResult Add() {
            ViewBag.Action = "add";
            var productViewModel = new ProductViewModel();
            productViewModel.Placements = placementRepository.GetPlacements().ToList();
            return View(productViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "admins")]
        public IActionResult Add(ProductViewModel productViewModel)
        {
            ViewBag.Action = "add";
            if (ModelState.IsValid) {
                productRepository.AddProduct(productViewModel.Product);
                return RedirectToAction(nameof(Index));
            }
            productViewModel.Placements = placementRepository.GetPlacements().ToList();
            return View(productViewModel);
        }

        [Authorize(Policy = "admins")]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "edit";
            var productViewModel = new ProductViewModel();
            var product = productRepository.GetProductById(id);
            if (product == null){
                return RedirectToAction(nameof(Index));
            }
            productViewModel.Product = product;
            productViewModel.Placements = placementRepository.GetPlacements().ToList();
            return View(productViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "admins")]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            ViewBag.Action = "edit";
            if (ModelState.IsValid)
            {
                productRepository.UpdateProduct(productViewModel.Product);
                return RedirectToAction(nameof(Index));
            }
            productViewModel.Placements = placementRepository.GetPlacements().ToList();
            return View(productViewModel);
        }

        [Authorize(Policy = "admins")]
        public IActionResult Delete(int id)
        {
            productRepository.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
