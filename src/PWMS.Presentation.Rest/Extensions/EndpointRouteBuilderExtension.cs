using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace PWMS.Presentation.Rest.Extensions;

public static class EndpointRouteBuilderExtension
{
    public static IEndpointRouteBuilder MapRestEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapControllers();
        return endpointRouteBuilder;
    }
}
