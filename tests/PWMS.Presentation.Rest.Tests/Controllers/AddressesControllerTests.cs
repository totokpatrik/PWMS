﻿using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using PWMS.Application.Addresses.Commands.Create;
using PWMS.Application.Addresses.Commands.Delete;
using PWMS.Application.Addresses.Commands.Update;
using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Paging;
using PWMS.Presentation.Rest.Models.Result;
using PWMS.Presentation.Rest.Tests.Common;
using RestSharp;
using System.Net;

namespace PWMS.Presentation.Rest.Tests.Controllers;

[Collection(nameof(RestCollectionDefinition))]
public class AddressesControllerTests
{
    private const string ApiUrlBaseV1 = "api/v1/addresses";
    private const string ApiUrlBaseV2 = "api/v2/addresses";

    private readonly RestWebApplicationFactory<Program> _factory;

    public AddressesControllerTests(RestWebApplicationFactory<Program> factory) => _factory = factory;

    private static class Get
    {
        public static string GetAddressByIdV1(Guid id) => $"{ApiUrlBaseV1}/{id}";
    }
    private static class Post
    {
        public static string GetPageToDoItem() => $"{ApiUrlBaseV1}/page";
        public static string CreateAddressV1() => $"{ApiUrlBaseV1}";
    }
    private static class Put
    {
        public static string UpdateAddressV1() => $"{ApiUrlBaseV1}";
    }
    public static class Delete
    {
        public static string DeleteAddressV1() => $"{ApiUrlBaseV1}";
    }

    [Fact]
    public async Task GetPageTestAsync()
    {
        var client = new RestClient(_factory.CreateClient()).Authenticate();

        var command = new PageContext(1, 10);

        var response = await client.PostAsync<ResultDto<CollectionViewModel<AddressDto>>>(
            new RestRequest(Post.GetPageToDoItem()).AddJsonBody(command));

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDto<CollectionViewModel<AddressDto>>>();
        response!.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeNull();
    }
    [Fact]
    public async Task GetPageTestAsync_WithSort()
    {
        var client = new RestClient(_factory.CreateClient()).Authenticate();

        var sortDescriptor = new SortDescriptor(field: "AddressLine", EnumSortDirection.Asc);
        var sort = new List<SortDescriptor>();
        sort.Add(sortDescriptor);

        var command = new PageContext(1, 10, null, sort);

        var response = await client.PostAsync<ResultDto<CollectionViewModel<AddressDto>>>(
            new RestRequest(Post.GetPageToDoItem()).AddJsonBody(command));

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDto<CollectionViewModel<AddressDto>>>();
        response!.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeNull();
    }
    [Fact]
    public async Task GetPageTestAsync_WithInvalidSort()
    {
        var client = new RestClient(_factory.CreateClient()).Authenticate();

        var sortDescriptor = new SortDescriptor(field: "Invalid", EnumSortDirection.Asc);
        var sort = new List<SortDescriptor>();
        sort.Add(sortDescriptor);

        var command = new PageContext(1, 10, null, sort);

        // Act
        Func<Task> act = () => client.PostAsync<ResultDto<CollectionViewModel<AddressDto>>>(
            new RestRequest(Post.GetPageToDoItem()).AddJsonBody(command));

        // Assert
        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => act());
        exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);

    }
    [Fact]
    public async Task GetByIdAsync()
    {
        var addreses = await GetAddresses();
        var client = new RestClient(_factory.CreateClient()).Authenticate();
        var response = await client.GetAsync<ResultDto<AddressDto>>(
            new RestRequest(Get.GetAddressByIdV1(addreses.Data.Data.First().Id)));

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDto<AddressDto>>();
        response!.IsSuccess.Should().BeTrue();
    }
    [Fact]
    public async Task CreateTestAsync()
    {
        var client = new RestClient(_factory.CreateClient()).Authenticate();

        var command = new CreateAddressCommand("Test Address");

        var response = await client.PostAsync<ResultDto<Guid>>(
            new RestRequest(Post.CreateAddressV1()).AddJsonBody(command));

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDto<Guid>>();
        response!.IsSuccess.Should().BeTrue();
    }
    [Fact]
    public async Task UpdateTestAsync()
    {
        var client = new RestClient(_factory.CreateClient()).Authenticate();
        var updatedAddressLine = "Updated test address";

        var command = new UpdateAddressCommand(Guid.Parse("4B178375-845F-4D84-9E5B-31A14F097AA1"), updatedAddressLine);

        var response = await client.PutAsync<ResultDto<AddressDto>>(
            new RestRequest(Put.UpdateAddressV1()).AddJsonBody(command));

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDto<AddressDto>>();
        response!.IsSuccess.Should().BeTrue();
        response.Data.AddressLine.Should().Be(updatedAddressLine);
    }
    [Fact]
    public async Task DeleteTestAsync()
    {
        var client = new RestClient(_factory.CreateClient()).Authenticate();
        var addressId = Guid.Parse("4B178375-845F-4D84-9E5B-31A14F097AA1");


        var command = new DeleteAddressCommand(addressId);

        var response = await client.DeleteAsync<ResultDto<Guid>>(
            new RestRequest(Delete.DeleteAddressV1()).AddJsonBody(command));

        response.Should().NotBeNull();
        response.Should().BeOfType<ResultDto<Guid>>();
        response!.IsSuccess.Should().BeTrue();
        response.Data.Should().Be(addressId);
    }

    private async Task<ResultDto<CollectionViewModel<AddressDto>>> GetAddresses()
    {
        var client = new RestClient(_factory.CreateClient()).Authenticate();

        var command = new PageContext(1, 100);

        var response = await client.PostAsync<ResultDto<CollectionViewModel<AddressDto>>>(
            new RestRequest(Post.GetPageToDoItem()).AddJsonBody(command));

        Assert.NotNull(response);

        return response;
    }
}
