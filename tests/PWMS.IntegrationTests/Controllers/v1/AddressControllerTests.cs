using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PWMS.Api.Models;
using PWMS.Application.Addresses.Commands.CreateAddress;
using PWMS.Core.Extensions;
using PWMS.Infrastructure.Data.Context;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Categories;

namespace PWMS.IntegrationTests.Controllers.v1;

[IntegrationTest]
public class AddressControllerTests : IAsyncLifetime
{
    private const string ConnectionString = "Data Source=:memory:";
    private const string Endpoint = "/api/addresses";
    private readonly SqliteConnection _eventStoreDbContextSqlite = new(ConnectionString);
    private readonly SqliteConnection _applicationDbContextSqlite = new(ConnectionString);

    private readonly ITestOutputHelper output;

    public AddressControllerTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    #region POST: /api/addresses/

    [Fact]
    public async Task Should_ReturnsHttpStatus200Ok_When_Post_ValidRequest()
    {
        // Arrange
        await using var webApplicationFactory = InitializeWebAppFactory();
        using var httpClient = webApplicationFactory.CreateClient(CreateClientOptions());

        var command = new Faker<CreateAddressCommand>()
            .RuleFor(command => command.AddressLine, faker => faker.Address.FullAddress())
            .Generate();

        var commandAsJsonString = command.ToJson();

        // Act
        using var jsonContent = new StringContent(commandAsJsonString, Encoding.UTF8, MediaTypeNames.Application.Json);
        using var act = await httpClient.PostAsync(Endpoint, jsonContent);

        // Assert (HTTP)
        act.Should().NotBeNull();
        act.IsSuccessStatusCode.Should().BeTrue();
        act.StatusCode.Should().Be(HttpStatusCode.OK);

        // Assert (HTTP Content Response)
        var response = (await act.Content.ReadAsStringAsync()).FromJson<ApiResponse<CreateAddressResponse>>();
        response.Should().NotBeNull();
        response.Success.Should().BeTrue();
        response.StatusCode.Should().Be(StatusCodes.Status200OK);
        response.Errors.Should().BeEmpty();
        response.Result.Should().NotBeNull();
        response.Result.Id.Should().NotBeEmpty();
    }
    [Theory]
    [InlineData(101)]
    [InlineData(200)]
    public async Task Should_ReturnsHttpStatus400BadRequest_When_Post_InvalidRequest_Too_Long(int numberOfCharacters)
    {
        // Arrange
        await using var webApplicationFactory = InitializeWebAppFactory();
        using var httpClient = webApplicationFactory.CreateClient(CreateClientOptions());

        var command = new Faker<CreateAddressCommand>()
            .RuleFor(command => command.AddressLine, faker => faker.Random.String(numberOfCharacters))
            .Generate();

        var commandAsJsonString = command.ToJson();

        // Act
        using var jsonContent = new StringContent(commandAsJsonString, Encoding.UTF8, MediaTypeNames.Application.Json);
        using var act = await httpClient.PostAsync(Endpoint, jsonContent);

        // Assert (HTTP)
        act.Should().NotBeNull();
        act.IsSuccessStatusCode.Should().BeFalse();
        act.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        // Assert (HTTP Content Response)
        var response = (await act.Content.ReadAsStringAsync()).FromJson<ApiResponse<CreateAddressResponse>>();
        response.Should().NotBeNull();
        response.Success.Should().BeFalse();
        response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        response.Result.Should().BeNull();
        response.Errors.Should().NotBeNullOrEmpty().And.OnlyHaveUniqueItems();
        response.Errors.FirstOrDefault().Message.Should().Be($"The length of 'AddressLine' must be 100 characters or fewer. You entered {numberOfCharacters} characters.");

        output.WriteLine(response.Errors.FirstOrDefault().Message);
    }

    [Fact]
    public async Task Should_ReturnsHttpStatus400BadRequest_When_Post_InvalidRequest()
    {
        // Arrange
        await using var webApplicationFactory = InitializeWebAppFactory();
        using var httpClient = webApplicationFactory.CreateClient(CreateClientOptions());

        var command = new Faker<CreateAddressCommand>().Generate();
        var commandAsJsonString = command.ToJson();

        // Act
        using var jsonContent = new StringContent(commandAsJsonString, Encoding.UTF8, MediaTypeNames.Application.Json);
        using var act = await httpClient.PostAsync(Endpoint, jsonContent);

        // Assert (HTTP)
        act.Should().NotBeNull();
        act.IsSuccessStatusCode.Should().BeFalse();
        act.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        // Assert (HTTP Content Response)
        var response = (await act.Content.ReadAsStringAsync()).FromJson<ApiResponse<CreateAddressResponse>>();
        response.Should().NotBeNull();
        response.Success.Should().BeFalse();
        response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        response.Result.Should().BeNull();
        response.Errors.Should().NotBeNullOrEmpty().And.OnlyHaveUniqueItems();
    }

    #endregion

    #region IAsyncLifetime

    public async Task InitializeAsync()
    {
        await _applicationDbContextSqlite.OpenAsync();
        await _eventStoreDbContextSqlite.OpenAsync();
    }

    public async Task DisposeAsync()
    {
        await _applicationDbContextSqlite.DisposeAsync();
        await _eventStoreDbContextSqlite.DisposeAsync();
    }

    #endregion

    #region Helpers

    private WebApplicationFactory<Program> InitializeWebAppFactory(
        Action<IServiceCollection> configureServices = null,
        Action<IServiceScope> configureServiceScope = null)
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(hostBuilder =>
            {
                hostBuilder.UseSetting("ConnectionStrings:SqlConnection", "InMemory");
                hostBuilder.UseSetting("ConnectionStrings:NoSqlConnection", "InMemory");
                hostBuilder.UseSetting("ConnectionStrings:CacheConnection", "InMemory");

                hostBuilder.UseSetting("CacheOptions:AbsoluteExpirationInHours", "1");
                hostBuilder.UseSetting("CacheOptions:SlidingExpirationInSeconds", "30");

                hostBuilder.UseEnvironment(Environments.Production);

                hostBuilder.ConfigureLogging(logging => logging.ClearProviders());

                hostBuilder.ConfigureServices(services =>
                {
                    services.RemoveAll<ApplicationDbContext>();
                    services.RemoveAll<DbContextOptions<ApplicationDbContext>>();
                    services.RemoveAll<EventStoreDbContext>();
                    services.RemoveAll<DbContextOptions<EventStoreDbContext>>();

                    services.AddDbContext<ApplicationDbContext>(
                        options => options.UseSqlite(_applicationDbContextSqlite));

                    services.AddDbContext<EventStoreDbContext>(
                        options => options.UseSqlite(_eventStoreDbContextSqlite));

                    configureServices?.Invoke(services);

                    using var serviceProvider = services.BuildServiceProvider(true);
                    using var serviceScope = serviceProvider.CreateScope();

                    var writeDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    writeDbContext.Database.EnsureCreated();

                    var eventStoreDbContext = serviceScope.ServiceProvider.GetRequiredService<EventStoreDbContext>();
                    eventStoreDbContext.Database.EnsureCreated();

                    configureServiceScope?.Invoke(serviceScope);

                    writeDbContext.Dispose();
                    eventStoreDbContext.Dispose();
                });
            });
    }

    private static WebApplicationFactoryClientOptions CreateClientOptions() => new() { AllowAutoRedirect = false };

    #endregion
}
