using Microsoft.AspNetCore.Mvc;
using ProjectApp.WebUI.DTOs.CategoryDtos;
using ProjectApp.WebUI.Services.CategoryServices;

namespace ProjectApp.WebUI.Controllers
{
    public class CategoryController(ICategoryService _categoryService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllAsync();
            return View(result.Data ?? []);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        {
            var result = await _categoryService.CreateAsync(command);

            if (!result.IsSuccessful)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(command);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateCategory(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);

            if (!result.IsSuccessful || result.Data is null)
            {
                return RedirectToAction(nameof(Index));
            }

            var command = new UpdateCategoryCommand(result.Data.Id, result.Data.Name);
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand command)
        {
            var result = await _categoryService.UpdateAsync(command);

            if (!result.IsSuccessful)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(command);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if (!result.IsSuccessful)
            {
                TempData["Error"] = result.Errors.FirstOrDefault()?.ErrorMessage ?? "Kategori silinemedi.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}