namespace PlantSales.API.Domain.Entities;

public class PotShape : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<BonsaiTree> BonsaiTrees { get; set; } = new List<BonsaiTree>();
}
