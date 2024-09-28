using MediatR;

namespace PWMS.Application.Common.CQRS;

public interface IQuery<out Tresponse> : IRequest<Tresponse>
    where Tresponse : notnull
{

}
