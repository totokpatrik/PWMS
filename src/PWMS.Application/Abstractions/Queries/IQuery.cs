using MediatR;

namespace PWMS.Application.Abstractions.Queries;

public interface IQuery<out Tresponse> : IRequest<Tresponse>
    where Tresponse : notnull
{

}
