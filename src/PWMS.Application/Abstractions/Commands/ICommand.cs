using MediatR;

namespace PWMS.Application.Abstractions.Commands;
public interface ICommand : ICommand<Unit>
{

}

public interface ICommand<out TResponse> : IRequest<TResponse>
{

}
