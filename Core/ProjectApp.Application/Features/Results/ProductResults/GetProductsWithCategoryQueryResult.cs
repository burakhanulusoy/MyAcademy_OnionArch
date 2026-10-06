using ProjectApp.Application.Features.Results.CategoryResults;

namespace ProjectApp.Application.Features.Results.ProductResults;

public record GetProductsWithCategoryQueryResult(int Id,
                                                 string Name,
                                                 decimal Price,
                                                 string  Description,
                                                 string? ImageUrl,
                                                 GetCategoriesQueryResult Category);

//Id gerek yok çunku GetCategoriesQueryResult ta var *
