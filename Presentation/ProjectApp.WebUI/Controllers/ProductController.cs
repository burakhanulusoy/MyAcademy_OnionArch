using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectApp.WebUI.DTOs.ProductDtos;
using ProjectApp.WebUI.Services.CategoryServices;
using ProjectApp.WebUI.Services.ProductServices;

namespace ProjectApp.WebUI.Controllers
{
    public class ProductController(IProductService _productService,
                                   ICategoryService _categoryService) : Controller
    {
        // Formlardaki kategori dropdown'ı için
        private async Task LoadCategoriesAsync()
        {
            var result = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(result.Data ?? [], "Id", "Name");
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAllProductWithCategoryAsync();
            return View(result.Data ?? []);
        }

        public async Task<IActionResult> CreateProduct()
        {
            await LoadCategoriesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            var result = await _productService.CreateAsync(command);

            if (!result.IsSuccessful)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                await LoadCategoriesAsync();
                return View(command);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var result = await _productService.GetByIdAsync(id);

            if (!result.IsSuccessful || result.Data is null)
            {
                return RedirectToAction(nameof(Index));
            }

            var p = result.Data;
            // ImageUrl dolu (mevcut görsel), Image null (henüz yeni dosya seçilmedi)
            var command = new UpdateProductCommand(p.Id, p.Name, p.Price, p.Description, p.CategoryId, p.ImageUrl, null);

            await LoadCategoriesAsync();
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
        {
            var result = await _productService.UpdateAsync(command);

            if (!result.IsSuccessful)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                await LoadCategoriesAsync();
                return View(command);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteAsync(id);

            if (!result.IsSuccessful)
            {
                TempData["Error"] = result.Errors.FirstOrDefault()?.ErrorMessage ?? "Ürün silinemedi.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}