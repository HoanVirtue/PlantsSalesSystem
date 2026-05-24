namespace PlantSales.API.Application.DTOs;

public class PlantDetailDto
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public long? TreeStyleId { get; set; }
    public string? TreeStyleName { get; set; }
    public string? PotShapeName { get; set; }
    public int? PotSizeCm { get; set; }
    public decimal? CanopyWidth { get; set; }
    public decimal? CanopyHeight { get; set; }
    public decimal? TrunkCircumference { get; set; }
    public int? FruitCount { get; set; }
    public decimal? ActualPrice { get; set; }
    public decimal? ImportPrice { get; set; }
    public string? DisplayPrice { get; set; }
    public string? PriceRangeLabel { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public string? CareInstruction { get; set; }
    public string? ThumbnailUrl { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsSold { get; set; }
    public List<PlantImageDto> Images { get; set; } = [];
}

public class PlantImageDto
{
    public long Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
}
