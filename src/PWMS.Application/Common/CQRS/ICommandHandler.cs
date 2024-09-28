﻿using MediatR;

namespace PWMS.Application.Common.CQRS;

public interface ICommandHandler<in TCommand>
    : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>
{

}
public interface ICommandHandler<in TCommand, Tresponse>
    : IRequestHandler<TCommand, Tresponse>
    where TCommand : ICommand<Tresponse>
    where Tresponse : notnull
{
}