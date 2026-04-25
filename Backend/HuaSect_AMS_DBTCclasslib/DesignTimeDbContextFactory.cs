using HuaSect_AMS_DBTCclasslib.DbCtx;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDatabaseCtx>
{
    public ApplicationDatabaseCtx CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDatabaseCtx>();
        
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        
        // If your DbContext needs IEncryptionService, you have two choices:
        // A) Pass null/dummy values for design-time (if encryption isn't needed for migrations)
        // B) Register services manually here
        
        return new ApplicationDatabaseCtx(optionsBuilder.Options);
    }
}