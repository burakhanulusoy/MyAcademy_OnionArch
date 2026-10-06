namespace ProjectApp.WebUI.DTOs.CategoryDtos;

public record UpdateCategoryCommand(int Id,
                                    string? Name);