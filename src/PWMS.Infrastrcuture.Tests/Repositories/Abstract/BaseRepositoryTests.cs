using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PWMS.Application.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using PWMS.Infrastructure.Data;
using PWMS.Domain.Addresses.Entities;
using PWMS.Domain.Tests.Builders;
using PWMS.Domain.Abstractions.Entities;

namespace PWMS.Infrastrcuture.Tests.Repositories.Abstract;

public abstract class BaseRepositoryTests : IAsyncLifetime
{
    private const string InMemoryConnectionString = "DataSource=:memory:";
    private readonly SqliteConnection _connection;
    protected readonly ApplicationDbContext Database;
    private readonly IContainer _container;
    protected readonly Address Address = new AddressBuilder().Build();

    public BaseRepositoryTests()
    {
        _connection = new SqliteConnection(InMemoryConnectionString);
        _connection.Open();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;

        var configuration = new ConfigurationBuilder().Build();
        var containerBuilder = new ContainerBuilder();

        var env = Mock.Of<IHostEnvironment>();
        containerBuilder.RegisterInstance(env);
        containerBuilder.RegisterInstance(Mock.Of<IMediator>());
        Database = new WeatherContext(options, env);
        Database.Database.EnsureCreated();

        containerBuilder.RegisterModule(new InfrastructureModule(options, configuration));
        _container = containerBuilder.Build();
    }

    public async Task InitializeAsync()
    {
        var addressRepository = GetRepository<Address>();
        addressRepository.Insert(Address);
        await GetUnitOfWork().CommitAsync();
    }

    public Task DisposeAsync()
    {
        Database.Dispose();
        _connection.Close();
        _connection.Dispose();
        return Task.CompletedTask;
    }

    protected IRepository<T> GetRepository<T>()
    where T : Aggregate
    {
        return _container.Resolve<IRepository<T>>();
    }

    protected IUnitOfWork GetUnitOfWork()
    {
        return _container.Resolve<IUnitOfWork>();
    }
}