﻿using MediatR;

namespace PWMS.Application.Abstractions.Commands;

public abstract record CommandBase<T> : IRequest<T>;
public abstract record Command : CommandBase<Unit>;
public abstract record CreateCommand : IRequest<Guid>;
