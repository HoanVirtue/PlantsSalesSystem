namespace PlantSales.API.Domain.Entities;

using PlantSales.API.Domain.Enums;

public class BonsaiTree : BaseEntity
{
    public string? Code { get; set; }
    public string Name { get; set; } = null!;
    public string? Slug { get; set; }
    public string? Description { get; set; }
    public string? ShortDescription { get; set; }
    public string? ThumbnailUrl { get; set; }

    public decimal? ImportPrice { get; set; }
    public decimal? ActualPrice { get; set; }
    public string? DisplayPrice { get; set; }

    public long? TreeStyleId { get; set; }
    public long? PotShapeId { get; set; }
    public long? PotSizeId { get; set; }

    public decimal? TrunkCircumference { get; set; }
    public decimal? CanopyWidth { get; set; }
    public decimal? CanopyHeight { get; set; }
    public int? FruitCount { get; set; }

    public string? CareInstruction { get; set; }

    public long ViewCount { get; set; } = 0;
    public bool IsFeatured { get; set; } = false;
    public bool IsSold { get; set; } = false;
    public TreeStatusEnum Status { get; set; } = TreeStatusEnum.Available;

    public DateTime? PublishedAt { get; set; }

    public virtual TreeStyle? TreeStyle { get; set; }
    public virtual PotShape? PotShape { get; set; }
    public virtual PotSize? PotSize { get; set; }
    public virtual ICollection<BonsaiImage> Images { get; set; } = new List<BonsaiImage>();
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
