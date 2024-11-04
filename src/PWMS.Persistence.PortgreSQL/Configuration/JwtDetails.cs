using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Persistence.PortgreSQL.Configuration;

public sealed record JwtDetails
{
    [NotNull]
    public string? Secret { get; init; }
    public int TokenExpirationInMinutes { get; init; }
    public string? Issuer { get; init; }
    public string? Audience { get; init; }
    public string Provider { get; init; } = default!;
}
