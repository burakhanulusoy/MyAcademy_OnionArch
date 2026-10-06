namespace ProjectApp.WebUI.DTOs.ProductDtos;

public record CreateProductCommand(string? Name,
                                   decimal? Price,
                                   string? Description,
                                   int? CategoryId,
                                   IFormFile? Image);