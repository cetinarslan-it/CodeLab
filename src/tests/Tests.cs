using System.Net;
using System.Text.Json;
using api.Data;
using api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace tests;

public class UnitTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    private readonly WebApplicationFactory<Program> factory;

    public UnitTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory.WithWebHostBuilder(builder =>
        {
            builder.UseTestServer();
            builder.ConfigureTestServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();
                services.AddDbContext<InsuranceDbContext>(options => 
                {
                    options.UseInMemoryDatabase("ApiInsuranceContextTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<InsuranceDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<WebApplicationFactory<Program>>>();
                    db.Database.EnsureCreated();
                    try
                    {
                        Seed(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the database with test data. Error: {ex.Message}");
                    }
                }
            });
        });
    }

    private void Seed(InsuranceDbContext db)
    {
        db.AddRange(
            new Insurance
            {
                InsuranceId = 1,
                Name = "insurance 1",
                Value = 10000
            },
            new Insurance
            {
                InsuranceId = 2,
                Name = "insurance 2",
                ParentId = 1,
                Value = 20000
            },
            new Insurance
            {
                InsuranceId = 3,
                Name = "insurance 3",
                ParentId = 2,
                Value = 30000
            },
            new Insurance
            {
                InsuranceId = 4,
                Name = "insurance 4",
                Value = 40000
            },
            new Insurance
            {
                InsuranceId = 5,
                Name = "insurance 5",
                ParentId = 4,
                Value = 50000
            },
            new Insurance
            {
                InsuranceId = 6,
                Name = "insurance 6",
                ParentId = 4,
                Value = 50000
            },
            new Insurance
            {
                InsuranceId = 7,
                Name = "insurance 7",
                ParentId = 0,
                Value = 10000
            },
            new Insurance
            {
                InsuranceId = 8,
                Name = "insurance 8",
                ParentId = 7,
                Value = 100000
            },
            new Insurance
            {
                InsuranceId = 9,
                Name = "insurance 9",
                ParentId = 8,
                Value = 100
            });
        db.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.factory?.Dispose();
        }
    }

    [Theory]
    [InlineData(3, 2, new [] { 140000, 110100, 100100 })]
    [InlineData(1, 3, new [] { 140000 })]
    [InlineData(2, 2, new [] { 140000, 110100 })]
    [InlineData(6, 2, new [] { 140000, 110100, 100100, 60000, 50000, 50000 })]
    [InlineData(4, 6, new [] { 140000, 110100, 100100, 60000 })]
  
    public async Task When_I_request_insurances_containing_highest_value_with_children_all_combined_I_expect_to_get_correct_values(int maxCount, int maxDepth, int[] expectedValues)
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync($"https://localhost:443/insurance/top/{maxCount}/{maxDepth}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var models = JsonSerializer.Deserialize<IEnumerable<int>>(body, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        models.Should().HaveCount(expectedValues.Count());
        models.Should().BeEquivalentTo(expectedValues);
    }
}