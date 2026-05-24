namespace PlantSales.API.Domain.Entities;

public class PotSize : BaseEntity
{
    public int SizeCm { get; set; }
    public string? Description { get; set; }

    public virtual ICollection<BonsaiTree> BonsaiTrees { get; set; } = new List<BonsaiTree>();
}
