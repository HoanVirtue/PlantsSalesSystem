using MediatR;
using Microsoft.EntityFrameworkCore;
using PlantSales.API.Application.DTOs;
using PlantSales.API.Application.Interfaces;
using PlantSales.API.Application.Features.Plants.Queries;
using PlantSales.API.Domain.Enums;

namespace PlantSales.API.Application.Features.Plants.Handlers;

public class GetPlantsQueryHandler : IRequestHandler<GetPlantsQuery, ApiResponseDto<PagedResultDto<PlantListDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPlantsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponseDto<PagedResultDto<PlantListDto>>> Handle(
        GetPlantsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var query = _unitOfWork.BonsaiTrees.GetQueryable()
                .Include(b => b.TreeStyle)
                .Include(b => b.PotShape)
                .Include(b => b.PotSize)
                .Include(b => b.Images)
                .Where(b => b.Status != TreeStatusEnum.Hidden);

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                var keyword = request.Keyword.Trim().ToLower();
                query = query.Where(b =>
                    b.Name.ToLower().Contains(keyword) ||
                    (b.Code != null && b.Code.ToLower().Contains(keyword)));
            }

            // Apply TreeStyleIds filter
            if (request.TreeStyleIds.Any())
            {
                query = query.Where(b => b.TreeStyleId.HasValue && request.TreeStyleIds.Contains(b.TreeStyleId.Value));
            }

            // Apply PotShapeIds filter
            if (request.PotShapeIds.Any())
            {
                query = query.Where(b => b.PotShapeId.HasValue && request.PotShapeIds.Contains(b.PotShapeId.Value));
            }

            // Apply PotSizeIds filter
            if (request.PotSizeIds.Any())
            {
                query = query.Where(b => b.PotSizeId.HasValue && request.PotSizeIds.Contains(b.PotSizeId.Value));
            }

            // Apply price range filter (static ranges)
            if (request.PriceRangeIds.Any())
            {
                bool hasRange1 = request.PriceRangeIds.Contains(1); // < 5M
                bool hasRange2 = request.PriceRangeIds.Contains(2); // 5M - 10M
                bool hasRange3 = request.PriceRangeIds.Contains(3); // 10M - 20M
                bool hasRange4 = request.PriceRangeIds.Contains(4); // >= 20M

                query = query.Where(b =>
                    (hasRange1 && b.ActualPrice < 5_000_000) ||
                    (hasRange2 && b.ActualPrice >= 5_000_000 && b.ActualPrice < 10_000_000) ||
                    (hasRange3 && b.ActualPrice >= 10_000_000 && b.ActualPrice < 20_000_000) ||
                    (hasRange4 && b.ActualPrice >= 20_000_000));
            }

            // Count total before pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Order and paginate
            var items = await query
                .OrderByDescending(b => b.IsFeatured)
                .ThenByDescending(b => b.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            // Map to DTO
            var dtos = items.Select(b => new PlantListDto
            {
                Id = b.Id,
                Code = b.Code,
                Name = b.Name,
                Slug = b.Slug,
                TreeStyleName = b.TreeStyle?.Name,
                PotShapeName = b.PotShape?.Name,
                PotSizeCm = b.PotSize?.SizeCm,
                ActualPrice = b.ActualPrice,
                DisplayPrice = b.DisplayPrice,
                PriceRangeLabel = GetPriceRangeLabel(b.ActualPrice),
                Status = b.Status.ToString(),
                ThumbnailUrl = b.Images.FirstOrDefault(img => img.IsThumbnail)?.ImageUrl ?? b.ThumbnailUrl,
                IsFeatured = b.IsFeatured
            }).ToList();

            var result = new PagedResultDto<PlantListDto>
            {
                Data = dtos,
                Total = totalCount,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize)
            };

            return new ApiResponseDto<PagedResultDto<PlantListDto>>
            {
                Success = true,
                Data = result,
                Message = "Lấy danh sách cây cảnh thành công"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<PagedResultDto<PlantListDto>>
            {
                Success = false,
                Message = "Lỗi khi lấy danh sách cây cảnh",
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
