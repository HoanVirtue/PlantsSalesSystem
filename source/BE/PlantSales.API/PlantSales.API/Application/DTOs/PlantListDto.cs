namespace PlantSales.API.Application.DTOs;

public class PlantListDto
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? TreeStyleName { get; set; }
    public string? PotShapeName { get; set; }
    public int? PotSizeCm { get; set; }
    public decimal? ActualPrice { get; set; }
    public string? DisplayPrice { get; set; }
    public string? PriceRangeLabel { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public bool IsFeatured { get; set; }
}
