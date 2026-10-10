using System.Text.Json.Serialization;

namespace ProjectApp.WebUI.DTOs.ProductDtos;

public record CreateProductCommand(string? Name,
                                   decimal? Price,
                                   string? Description,
                                   int? CategoryId,
                                   string? ImageUrl,                          // API'ye bu gidecek
                                   [property: JsonIgnore] IFormFile? Image); // formdan gelen dosya, JSON'a girmesin