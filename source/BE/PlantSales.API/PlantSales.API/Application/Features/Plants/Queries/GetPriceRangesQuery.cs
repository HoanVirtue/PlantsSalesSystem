namespace PlantSales.API.Application.Features.Plants.Queries;

using MediatR;
using PlantSales.API.Application.DTOs;

public class GetPriceRangesQuery : IRequest<ApiResponseDto<List<PriceRangeDto>>>
{
}
