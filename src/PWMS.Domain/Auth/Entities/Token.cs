using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Domain.Auth.Entities;

public sealed class Token
{
    public string TokenString { get; set; } = default!;
    public DateTime Expiration { get; set; }
}
