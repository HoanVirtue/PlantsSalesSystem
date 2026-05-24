namespace PlantSales.API.Application.Features.Plants.Queries;

using MediatR;
using PlantSales.API.Application.DTOs;

public class GetPlantBySlugQuery : IRequest<ApiResponseDto<PlantDetailDto?>>
{
    public string Slug { get; set; } = string.Empty;
}
