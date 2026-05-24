namespace PlantSales.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlantSales.API.Application.DTOs;
using PlantSales.API.Application.Features.Plants.Queries;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("price-ranges")]
    public async Task<ActionResult<ApiResponseDto<List<PriceRangeDto>>>> GetPriceRanges(
        CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetPriceRangesQuery(), cancellationToken);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponseDto<CategoriesDto>>> GetCategories(
        CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetCategoriesQuery(), cancellationToken);
    }

    [HttpGet("meta")]
    public IActionResult GetMeta()
    {
        var meta = new
        {
            TreeShapes = new[]
            {
                new { value = 0, label = "Ban Tra" },
                new { value = 1, label = "Truc" },
                new { value = 2, label = "Tan Rong" }
            },
            PotStyles = new[]
            {
                new { value = 0, label = "Tron" },
                new { value = 1, label = "Vuong" },
                new { value = 2, label = "Chu Nhat" }
            },
            PlantStatus = new[]
            {
                new { value = 0, label = "Available" },
                new { value = 1, label = "Reserved" },
                new { value = 2, label = "Sold" }
            }
        };

        return Ok(new ApiResponseDto<object>
        {
            Success = true,
            Data = meta,
            Message = "Success"
        });
    }
}
