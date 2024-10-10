using MediatR;

namespace PWMS.Application.Tests.Common;

public sealed class QueryTestFixture : IDisposable
{
    public IMediator Mediator { get; }
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
