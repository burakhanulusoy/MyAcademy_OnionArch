namespace ProjectApp.WebUI.DTOs.ProductDtos;


public record UpdateProductCommand(int Id,
                                   string? Name,
                                   decimal? Price,
                                   string? Description,
                                   int? CategoryId,
                                   string? ImageUrl,   // formda mevcut görseli göstermek için
                                   IFormFile? Image);  // yeni görsel seçilirse




