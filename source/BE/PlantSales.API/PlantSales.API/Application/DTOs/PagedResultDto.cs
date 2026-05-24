namespace PlantSales.API.Application.DTOs;

public class PagedResultDto<T>
{
    public List<T> Data { get; set; } = new();
    public long Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}
