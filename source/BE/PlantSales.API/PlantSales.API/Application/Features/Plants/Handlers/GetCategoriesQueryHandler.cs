using MediatR;
using PlantSales.API.Application.DTOs;
using PlantSales.API.Application.Interfaces;
using PlantSales.API.Application.Features.Plants.Queries;

namespace PlantSales.API.Application.Features.Plants.Handlers;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, ApiResponseDto<CategoriesDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoriesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponseDto<CategoriesDto>> Handle(
        GetCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var treeStyles = await _unitOfWork.TreeStyles.GetListAsync(cancellationToken);
            var potShapes = await _unitOfWork.PotShapes.GetListAsync(cancellationToken);
            var potSizes = await _unitOfWork.PotSizes.GetListAsync(cancellationToken);

            var dto = new CategoriesDto
            {
                TreeStyles = treeStyles.Select(ts => new TreeStyleDto
                {
                    Id = ts.Id,
                    Name = ts.Name,
                    Slug = ts.Slug,
                    Description = ts.Description
                }).ToList(),
                PotShapes = potShapes.Select(ps => new PotShapeDto
                {
                    Id = ps.Id,
                    Name = ps.Name,
                    Description = ps.Description
                }).ToList(),
                PotSizes = potSizes.Select(pz => new PotSizeDto
                {
                    Id = pz.Id,
                    SizeCm = pz.SizeCm,
                    Description = pz.Description
                }).ToList()
            };

            return new ApiResponseDto<CategoriesDto>
            {
                Success = true,
                Data = dto,
                Message = "Lấy danh mục thành công"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<CategoriesDto>
            {
                Success = false,
                Message = "Lỗi khi lấy danh mục",
                Errors = new List<string> { ex.Message }
            };
        }
    }
}
