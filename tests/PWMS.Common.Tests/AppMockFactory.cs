using PWMS.Application.Addresses.Repositories;

namespace PWMS.Common.Tests;

public static class AppMockFactory
{
    private static readonly Lazy<MockRepository> MockRepository = new Lazy<MockRepository>(() => new MockRepository(MockBehavior.Default));

    private static MockRepository MockRepositoryInstance => MockRepository.Value;

    public static ICurrentUserService CreateCurrentUserServiceMock()
    {
        ICurrentUser currentUser = new CurrentUser
        {
            Id = "1"
        };

        return MockRepositoryInstance
            .Of<ICurrentUserService>().First(x => x.GetCurrentUser() == currentUser);
    }

    public static IAddressRepository CreateMockAddressRepository()
    {
        var addressRepository = MockRepositoryInstance.Create<IAddressRepository>();
        addressRepository
            .Setup(x => x.AddAsync(new Domain.Addresses.Entities.Address("TestAddress"),CancellationToken.None))
            .Callback(() => Thread.Sleep(10000));

        return MockRepositoryInstance
            .Of<IAddressRepository>().First();
    }

    public static IMediator CreateMediatorMock()
    {
        var mediator = MockRepositoryInstance.Create<IMediator>();
        mediator.Setup(x => x.Publish(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Verifiable(string.Empty);

        return MockRepositoryInstance.Of<IMediator>().First();
    }
}
