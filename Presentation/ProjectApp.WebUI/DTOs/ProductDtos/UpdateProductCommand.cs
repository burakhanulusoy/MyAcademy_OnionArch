using System.Text.Json.Serialization;

namespace ProjectApp.WebUI.DTOs.ProductDtos;

public record UpdateProductCommand(int Id,
                                   string? Name,
                                   decimal? Price,
                                   string? Description,
                                   int? CategoryId,
                                   string? ImageUrl,                          // mevcut görsel (veya yenisi)
                                   [property: JsonIgnore] IFormFile? Image); // yeni görsel seçilirse