using PlatformService.Models;

namespace PlatformService.Data.Repository;

// not using the repo since this class will be used for migrations and I don't want to add new methods in the repo
public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
    }

    private static void SeedData(AppDbContext? ctx)
    {
        ArgumentNullException.ThrowIfNull(nameof(ctx));

        // if the  platforms table is empty
        if (!ctx.Platforms.Any())
        {
            Console.WriteLine("Seeding data");

            ctx.Platforms.AddRange(
                new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                );
            ctx.SaveChanges();
        }
        else
        {
            Console.WriteLine("The Platforms table already has data.");
        }
    }
}