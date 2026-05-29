using Microsoft.EntityFrameworkCore;
using WarehouseSystemTest.Infrastructure.Database;
using WarehouseSystemTest.Domain.Warehouse.Repositories;
using WarehouseSystemTest.Domain.Warehouse.Services;

namespace WarehouseSystemTest;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<WarehouseService>();
        builder.Services.AddScoped<WarehouseQueryRepository>();
        builder.Services.AddScoped<WarehouseStoreRepository>();
        builder.Services.AddOpenApiDocument(options =>
        {
            options.Title = "Backend Fullstack Test API";
            options.Version = "v1";
        });

        builder.Services.AddDbContext<DatabaseContext>(options =>
        {
            var connectionString = builder.Configuration["ConnectionString:todo"];

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Missing database connection string: ConnectionString:todo");
            }

            options.UseSqlServer(connectionString);
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
            app.UseSwaggerUi();
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}
