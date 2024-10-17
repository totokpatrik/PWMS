using PWMS.Application.Common.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Application.Addresses.Models;

public sealed partial record AddressFilter
{
    public FilterFieldDefinition<Guid>? Id { get; set; }
    public FilterFieldDefinition<string>? AddressLine { get; set; }
}
