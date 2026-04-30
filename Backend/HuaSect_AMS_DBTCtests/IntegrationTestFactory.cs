using System.Security.Claims;
using HuaSect_AMS_DBTC.Service;
using HuaSect_AMS_DBTCclasslib.DbCtx;
using HuaSect_AMS_DBTCclasslib.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Testcontainers.PostgreSql;

namespace HuaSect_AMS_DBTCtests;

public class IntegrationTestFactory : WebApplicationFactory<Program>
{
    private readonly PostgreSqlContainer _postgresContainer;

    public string TestConnectionString { get; private set; } = null!;

    public IntegrationTestFactory()
    {
        _postgresContainer = new PostgreSqlBuilder("postgres:18-alpine")
            .WithDatabase("test_attendance_db")
            .WithUsername("postgres")
            .WithPassword("TestPassword123!")
            .WithCleanUp(true)
            .Build();

    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(TestAuthHandler.SchemeName)
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                    TestAuthHandler.SchemeName,
                    options => { });

            services.AddAuthorizationBuilder()
                .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
                .AddPolicy("EmployeeOnly", policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c => c.Type == "employee_id")));

            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDatabaseCtx>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.RemoveAll<ApplicationDatabaseCtx>();

            services.AddDbContext<ApplicationDatabaseCtx>(options =>
            {
                options.UseNpgsql(TestConnectionString, npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(ApplicationDatabaseCtx).Assembly.FullName);
                    npgsqlOptions.EnableRetryOnFailure(0);
                });
                options.EnableSensitiveDataLogging();
                options.LogTo(Console.WriteLine, LogLevel.Information);
            });

            var encryptionDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(IEncryptionService));
            if (encryptionDescriptor != null)
            {
                services.Remove(encryptionDescriptor);
            }

            var testEncryptionKey = "test_key_32_bytes_long_for_aes256!!";
            services.AddSingleton<IEncryptionService>(
                new AesEncryptionService(testEncryptionKey));

            services.RemoveAll<IEmailSender>();
            services.AddTransient<IEmailService, EmailSenderService>();

            RemoveAllHostedServices(services);
        });
    }

    public async Task InitializeAsync()
    {
        await _postgresContainer.StartAsync();
        TestConnectionString = _postgresContainer.GetConnectionString();

        await using var scope = Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDatabaseCtx>();

        await dbContext.Database.MigrateAsync();

        await SeedTestDataAsync(dbContext);
    }

    public new async Task DisposeAsync()
    {
        await _postgresContainer.DisposeAsync();

        await base.DisposeAsync();
    }

    public HttpClient CreateClientWithAuth(params Claim[] claims)
    {
        var client = CreateClient();
        
        var authHandler = Services.GetRequiredService<TestAuthHandler>();
        authHandler.TestUser = new ClaimsPrincipal(
            new ClaimsIdentity(claims, TestAuthHandler.SchemeName));
        
        return client;
    }

    public NpgsqlConnection CreateRawConnection()
    {
        return new NpgsqlConnection(TestConnectionString);
    }

    private static void RemoveAllHostedServices(IServiceCollection services)
    {
        var hostedServiceDescriptors = services
            .Where(d => d.ServiceType == typeof(IHostedService) || 
                       d.ImplementationType?.GetInterfaces().Contains(typeof(IHostedService)) == true)
            .ToList();

        foreach (var descriptor in hostedServiceDescriptors)
        {
            services.Remove(descriptor);
        }
    }

    private static async Task SeedTestDataAsync(ApplicationDatabaseCtx context)
    {
        await Task.CompletedTask;
    }
}