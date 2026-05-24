namespace PlantSales.API.Domain.Entities;

public class TreeStyle : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Slug { get; set; }
    public string? Description { get; set; }

    public virtual ICollection<BonsaiTree> BonsaiTrees { get; set; } = new List<BonsaiTree>();
}
