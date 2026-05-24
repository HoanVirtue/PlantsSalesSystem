namespace PlantSales.API.Application.Features.Plants.Queries;

using MediatR;
using PlantSales.API.Application.DTOs;

public class GetPlantsQuery : IRequest<ApiResponseDto<PagedResultDto<PlantListDto>>>
{
    public string? Keyword { get; set; }
    public List<int> PriceRangeIds { get; set; } = [];
    public List<long> TreeStyleIds { get; set; } = [];
    public List<long> PotShapeIds { get; set; } = [];
    public List<long> PotSizeIds { get; set; } = [];
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 12;
}
