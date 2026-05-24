namespace PlantSales.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantSales.API.Application.DTOs;
using PlantSales.API.Application.Features.Plants.Queries;

[ApiController]
[Route("api/v1/[controller]")]
public class PlantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponseDto<PagedResultDto<PlantListDto>>>> GetPlants(
        [FromQuery] string? keyword,
        [FromQuery] string? priceRangeIds,
        [FromQuery] string? treeStyleIds,
        [FromQuery] string? potShapeIds,
        [FromQuery] string? potSizeIds,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 12,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPlantsQuery
        {
            Keyword = keyword,
            PriceRangeIds = ParseIntList(priceRangeIds),
            TreeStyleIds = ParseLongList(treeStyleIds),
            PotShapeIds = ParseLongList(potShapeIds),
            PotSizeIds = ParseLongList(potSizeIds),
            Page = page,
            PageSize = pageSize
        };

        return await _mediator.Send(query, cancellationToken);
    }

    [HttpGet("slug/{slug}")]
    public async Task<ActionResult<ApiResponseDto<PlantDetailDto?>>> GetPlantBySlug(
        string slug,
        CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetPlantBySlugQuery { Slug = slug }, cancellationToken);
    }

    private static List<int> ParseIntList(string? values)
    {
        if (string.IsNullOrEmpty(values))
            return [];

        return values.Split(',')
            .Where(v => int.TryParse(v, out _))
            .Select(int.Parse)
            .ToList();
    }

    private static List<long> ParseLongList(string? values)
    {
        if (string.IsNullOrEmpty(values))
            return [];

        return values.Split(',')
            .Where(v => long.TryParse(v, out _))
            .Select(long.Parse)
            .ToList();
    }
}
