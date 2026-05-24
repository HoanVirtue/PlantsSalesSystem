namespace PlantSales.API.Application.DTOs;

public class BonsaiImageDto
{
    public long Id { get; set; }
    public string ImageUrl { get; set; } = null!;
    public bool IsThumbnail { get; set; }
    public int SortOrder { get; set; }
}
