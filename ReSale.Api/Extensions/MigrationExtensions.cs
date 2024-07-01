using Microsoft.EntityFrameworkCore;
using ReSale.Infrastructure.Persistence;

namespace ReSale.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ReSaleDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<ReSaleDbContext>();

        dbContext.Database.Migrate();
    }
}
