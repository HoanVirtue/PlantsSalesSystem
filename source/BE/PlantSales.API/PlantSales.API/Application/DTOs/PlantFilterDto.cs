namespace PlantSales.API.Application.DTOs;

public class PlantFilterDto
{
    public string? Keyword { get; set; }
    public List<int> PriceRangeIds { get; set; } = [];
    public List<int> TreeShapes { get; set; } = [];
    public List<int> PotStyles { get; set; } = [];
    public List<int> PotSizes { get; set; } = [];
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 12;
}
