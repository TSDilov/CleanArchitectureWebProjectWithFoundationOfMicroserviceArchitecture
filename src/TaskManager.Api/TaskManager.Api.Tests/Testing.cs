using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TaskManager.Api.Tests.Helpers;
using Testcontainers.PostgreSql;

namespace TaskManager.Api.Tests;

[SetUpFixture]
public class Testing
{
    private PostgreSqlContainer _postgres;

    [OneTimeSetUp]
    public async Task RunBeforeAnyTests()
    {
        _postgres = new PostgreSqlBuilder()
            .WithImage("postgres")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .WithPortBinding(9999, 5432)
            .Build();

        await _postgres.StartAsync();
        var application = new CustomWebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
                builder
                .UseEnvironment("CI")
                .ConfigureServices((ctx, services) =>
                {
                    Configuration = ctx.Configuration;
                    ScopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
                })
        );

        Client = application.CreateClient();
    }

    [OneTimeTearDown]
    public async Task RunAfterAllTests()
    {
        await _postgres.DisposeAsync();
    }

    public static IConfiguration Configuration { get; private set; }

    public static IServiceScopeFactory ScopeFactory { get; private set; }

    public static HttpClient Client { get; private set; }
}