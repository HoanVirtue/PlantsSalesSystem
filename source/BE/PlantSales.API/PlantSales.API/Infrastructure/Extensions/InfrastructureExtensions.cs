namespace PlantSales.API.Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;
using PlantSales.API.Application.Interfaces;
using PlantSales.API.Infrastructure.Data;
using PlantSales.API.Infrastructure.Repositories;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
