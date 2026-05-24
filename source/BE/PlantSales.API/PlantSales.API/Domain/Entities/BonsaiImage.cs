namespace PlantSales.API.Domain.Entities;

public class BonsaiImage : BaseEntity
{
    public long BonsaiTreeId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public bool IsThumbnail { get; set; } = false;
    public int SortOrder { get; set; } = 0;

    public virtual BonsaiTree BonsaiTree { get; set; } = null!;
}
