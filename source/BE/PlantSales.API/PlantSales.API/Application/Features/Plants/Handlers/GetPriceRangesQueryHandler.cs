using MediatR;
using PlantSales.API.Application.DTOs;
using PlantSales.API.Application.Features.Plants.Queries;

namespace PlantSales.API.Application.Features.Plants.Handlers;

public class GetPriceRangesQueryHandler : IRequestHandler<GetPriceRangesQuery, ApiResponseDto<List<PriceRangeDto>>>
{
    public Task<ApiResponseDto<List<PriceRangeDto>>> Handle(
        GetPriceRangesQuery request,
        CancellationToken cancellationToken)
    {
        var ranges = new List<PriceRangeDto>
        {
            new() { Id = 1, Label = "Dưới 5 triệu", MinPrice = 0, MaxPrice = 5_000_000 },
            new() { Id = 2, Label = "5 - 10 triệu", MinPrice = 5_000_000, MaxPrice = 10_000_000 },
            new() { Id = 3, Label = "10 - 20 triệu", MinPrice = 10_000_000, MaxPrice = 20_000_000 },
            new() { Id = 4, Label = "Trên 20 triệu", MinPrice = 20_000_000, MaxPrice = 999_000_000 }
        };

        var response = new ApiResponseDto<List<PriceRangeDto>>
        {
            Success = true,
            Data = ranges,
            Message = "Lấy danh sách khoảng giá thành công"
        };

        return Task.FromResult(response);
    }
}
