namespace PlantSales.API.Application.DTOs;

public class BonsaiTreeListDto
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string Name { get; set; } = null!;
    public string? Slug { get; set; }
    public string? DisplayPrice { get; set; }
    public string Status { get; set; } = null!;
    public string? ThumbnailUrl { get; set; }
    public string? TreeStyleName { get; set; }
    public string? PotShapeName { get; set; }
    public string? PotSizeLabel { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsSold { get; set; }
}
