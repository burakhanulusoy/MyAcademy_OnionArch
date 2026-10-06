namespace ProjectApp.Application.Features.Results.ProductResults;

public record GetProductByIdQueryResult(int Id,
                                             string Name,
                                             decimal Price,
                                             string  Description,
                                             string? ImageUrl);
