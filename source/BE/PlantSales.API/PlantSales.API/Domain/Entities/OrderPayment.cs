namespace PlantSales.API.Domain.Entities;

public class OrderPayment : BaseEntity
{
    public long OrderId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public long CreatedBy { get; set; }

    public virtual Order Order { get; set; } = null!;
    public virtual User CreatedByUser { get; set; } = null!;
}
