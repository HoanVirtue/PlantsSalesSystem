namespace PlantSales.API.Application.DTOs;

public class BonsaiTreeFilterDto
{
    public string? Keyword { get; set; }
    public List<long>? TreeStyleIds { get; set; }
    public List<long>? PotShapeIds { get; set; }
    public List<long>? PotSizeIds { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? Status { get; set; }
    public bool? IsFeatured { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 12;
}
