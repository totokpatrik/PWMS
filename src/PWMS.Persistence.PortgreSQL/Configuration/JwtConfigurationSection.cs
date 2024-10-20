using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Persistence.PortgreSQL.Configuration;
internal sealed record JwtConfigurationSection
{
    public const string SectionName = "JwtSettings";

    public JwtConfigurationSection()
    { }

    public JwtConfigurationSection(JwtDetails jwtDetails) => JwtDetails = JwtDetails;

    public JwtDetails? JwtDetails { get; init; }
}
