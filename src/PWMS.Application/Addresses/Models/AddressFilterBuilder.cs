using PWMS.Application.Common.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Application.Addresses.Models;

public sealed partial record AddressFilter
{
    public sealed class AddressFilterBuilder
    {
        private readonly AddressFilter _filter;

        public AddressFilterBuilder() => _filter = new AddressFilter();

        public AddressFilterBuilder Id(Guid id)
        {
            _filter.Id = new FilterFieldDefinition<Guid>() { Value = id };
            return this;
        }

        public AddressFilterBuilder AddressLine(string addressLine)
        {
            _filter.AddressLine = new FilterFieldDefinition<string>() { Value = addressLine };
            return this;
        }

        public AddressFilter Build() => _filter;
    }

    public static AddressFilterBuilder CreateBuilder() => new();
}
