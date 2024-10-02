using MediatR;

namespace PWMS.Application.Abstractions.Queries;

public abstract record Query<T> : IRequest<T>;