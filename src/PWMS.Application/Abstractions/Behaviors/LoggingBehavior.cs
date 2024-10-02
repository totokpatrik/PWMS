﻿using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace PWMS.Application.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull

{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request={Request} - response={Response} - request data={RequestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken}",
                typeof(TRequest).Name, timeTaken.Seconds);
        }

        logger.LogInformation("[END] Handled {Request} with {Response}",
            typeof(TRequest).Name, typeof(TResponse).Name);
        return response;
    }
}
