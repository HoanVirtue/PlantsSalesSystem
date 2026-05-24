namespace PlantSales.API.Application.DTOs;

public class CategoriesDto
{
    public List<TreeStyleDto> TreeStyles { get; set; } = new();
    public List<PotShapeDto> PotShapes { get; set; } = new();
    public List<PotSizeDto> PotSizes { get; set; } = new();
}
