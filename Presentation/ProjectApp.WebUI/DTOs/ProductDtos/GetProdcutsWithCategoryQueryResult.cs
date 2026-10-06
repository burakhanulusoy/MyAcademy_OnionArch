using ProjectApp.WebUI.DTOs.CategoryDtos;

namespace ProjectApp.WebUI.DTOs.ProductDtos;

public record GetProductsWithCategoryQueryResult(int Id,
                                                 string Name,
                                                 decimal Price,
                                                 string Description,
                                                 string? ImageUrl,
                                                 GetCategoriesQueryResult? Category);