using MediatR;
using PlantSales.API.Application.DTOs;

namespace PlantSales.API.Application.Features.Plants.Queries;

public class GetCategoriesQuery : IRequest<ApiResponseDto<CategoriesDto>>
{
}
