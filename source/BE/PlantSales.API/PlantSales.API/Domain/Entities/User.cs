namespace PlantSales.API.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? AvatarUrl { get; set; }
    public bool IsActive { get; set; } = true;
}
