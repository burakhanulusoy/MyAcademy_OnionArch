namespace ProjectApp.WebUI.DTOs.ProductDtos;

public record GetProductByIdQueryResult(int Id,
                                        string Name,
                                        decimal Price,
                                        string Description,
                                        string? ImageUrl,
                                        int CategoryId);