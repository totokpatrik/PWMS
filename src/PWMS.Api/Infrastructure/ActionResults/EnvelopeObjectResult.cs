﻿namespace PWMS.Api.Infrastructure.ActionResults;

public class EnvelopeObjectResult : ObjectResult
{
    public EnvelopeObjectResult(Envelope envelope)
        : base(envelope)
    {
        StatusCode = envelope.Status;
    }
}
