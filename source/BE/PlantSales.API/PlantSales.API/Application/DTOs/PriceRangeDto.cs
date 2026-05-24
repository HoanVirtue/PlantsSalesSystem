namespace PlantSales.API.Application.DTOs;

public class PriceRangeDto
{
    public int Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
}
