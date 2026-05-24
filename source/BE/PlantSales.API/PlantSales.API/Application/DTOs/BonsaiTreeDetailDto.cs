namespace PlantSales.API.Application.DTOs;

public class BonsaiTreeDetailDto
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string Name { get; set; } = null!;
    public string? Slug { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public string? DisplayPrice { get; set; }
    public decimal? ImportPrice { get; set; }
    public decimal? ActualPrice { get; set; }
    public string? ThumbnailUrl { get; set; }
    public decimal? TrunkCircumference { get; set; }
    public decimal? CanopyWidth { get; set; }
    public decimal? CanopyHeight { get; set; }
    public int? FruitCount { get; set; }
    public string? CareInstruction { get; set; }
    public string Status { get; set; } = null!;
    public bool IsFeatured { get; set; }
    public bool IsSold { get; set; }
    public long ViewCount { get; set; }

    public TreeStyleDto? TreeStyle { get; set; }
    public PotShapeDto? PotShape { get; set; }
    public PotSizeDto? PotSize { get; set; }
    public List<BonsaiImageDto> Images { get; set; } = new();
}
