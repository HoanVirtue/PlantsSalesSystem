using MediatR;
using Microsoft.EntityFrameworkCore;
using PlantSales.API.Application.DTOs;
using PlantSales.API.Application.Interfaces;
using PlantSales.API.Application.Features.Plants.Queries;

namespace PlantSales.API.Application.Features.Plants.Handlers;

public class GetPlantBySlugQueryHandler : IRequestHandler<GetPlantBySlugQuery, ApiResponseDto<PlantDetailDto?>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPlantBySlugQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponseDto<PlantDetailDto?>> Handle(
        GetPlantBySlugQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var tree = await _unitOfWork.BonsaiTrees.GetQueryable()
                .Include(b => b.TreeStyle)
                .Include(b => b.PotShape)
                .Include(b => b.PotSize)
                .Include(b => b.Images)
                .FirstOrDefaultAsync(b => b.Slug == request.Slug, cancellationToken);

            if (tree == null)
            {
                return new ApiResponseDto<PlantDetailDto?>
                {
                    Success = false,
                    Data = null,
                    Message = "Không tìm thấy cây cảnh"
                };
            }

            var dto = new PlantDetailDto
            {
                Id = tree.Id,
                Code = tree.Code,
                Name = tree.Name,
                Slug = tree.Slug,
                TreeStyleId = tree.TreeStyleId,
                TreeStyleName = tree.TreeStyle?.Name,
                PotShapeName = tree.PotShape?.Name,
                PotSizeCm = tree.PotSize?.SizeCm,
                CanopyWidth = tree.CanopyWidth,
                CanopyHeight = tree.CanopyHeight,
                TrunkCircumference = tree.TrunkCircumference,
                FruitCount = tree.FruitCount,
                ActualPrice = tree.ActualPrice,
                ImportPrice = tree.ImportPrice,
                DisplayPrice = tree.DisplayPrice,
                PriceRangeLabel = GetPriceRangeLabel(tree.ActualPrice),
                Status = tree.Status.ToString(),
                Description = tree.Description,
                ShortDescription = tree.ShortDescription,
                CareInstruction = tree.CareInstruction,
                ThumbnailUrl = tree.Images.FirstOrDefault(img => img.IsThumbnail)?.ImageUrl ?? tree.ThumbnailUrl,
                IsFeatured = tree.IsFeatured,
                IsSold = tree.IsSold,
                Images = tree.Images.Select(img => new PlantImageDto
                {
                    Id = img.Id,
                    ImageUrl = img.ImageUrl,
                    IsPrimary = img.IsThumbnail
                }).ToList()
            };

            return new ApiResponseDto<PlantDetailDto?>
            {
                Success = true,
                Data = dto,
                Message = "Lấy chi tiết cây cảnh thành công"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<PlantDetailDto?>
            {
                Success = false,
                Data = null,
                Message = "Lỗi khi lấy chi tiết cây cảnh",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    private static string GetPriceRangeLabel(decimal? price)
    {
        if (price == null) return string.Empty;

        return price < 5_000_000 ? "Dưới 5 triệu" :
               price < 10_000_000 ? "5 - 10 triệu" :
               price < 20_000_000 ? "10 - 20 triệu" :
               "Trên 20 triệu";
    }
}
