namespace PlantSales.API.Application.DTOs;

public class TreeStyleDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Slug { get; set; }
    public string? Description { get; set; }
}
