namespace CarRent.Domain.Common.DTOs;

public class CategoryDto
{
    public string Value { get; set; } = null!;
    public CategoryType Type { get; set; }
    public List<CategoryDto>? Children { get; set; }
}
