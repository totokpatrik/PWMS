﻿using PWMS.Application.Addresses.Models;
using PWMS.Application.Common.Filters;
using PWMS.Application.Common.Paging;
using PWMS.Domain.Addresses.Entities;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Application.Addresses.Specifications;

internal sealed class AddressSpecification : Specification<Address>
{
    private static readonly FrozenDictionary<string, Expression<Func<Address, object>>> SortExpressions =
        new Dictionary<string, Expression<Func<Address, object>>>
        {
            { nameof(AddressFilter.Id), c => c.Id },
            { nameof(AddressFilter.AddressLine), c => c.AddressLine },
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

    private AddressSpecification()
    {
    }

    public static Specification<Address> Create()
    {
        var specification = new AddressSpecification();

        var specificationBuilder = specification.Query;

        specificationBuilder.AsNoTracking();
        return specification;
    }

    public static Specification<Address> Create(IPageContext<AddressFilter> pageContext)
    {
        var specification = new AddressSpecification();

        var specificationBuilder = specification.Query;

        Filter(specificationBuilder, pageContext.Filter);
        specification.Sort(specificationBuilder, pageContext.ListSort);

        if (pageContext.PageIndex != 0)
        {
            specificationBuilder.Skip(pageContext.PageSize * (pageContext.PageIndex - 1));
        }

        if (pageContext.PageSize != 0)
        {
            specificationBuilder.Take(pageContext.PageSize);
        }

        specificationBuilder.AsNoTracking();
        return specification;
    }

    private static void Filter(ISpecificationBuilder<Address> specificationBuilder, AddressFilter filter)
    {
        if (filter.AddressLine.HasValue())
        {
            specificationBuilder.Where(x => x.AddressLine == filter.AddressLine.Value);
        }

        if (filter.Id.HasValue())
        {
            specificationBuilder.Where(x => x.Id == filter.Id.Value);
        }
    }

    private ISpecificationBuilder<Address> Sort(ISpecificationBuilder<Address> specificationBuilder,
        IEnumerable<SortDescriptor> sorts)
    {
        var sortDescriptors = sorts as SortDescriptor[] ?? sorts.ToArray();

        if (sortDescriptors.Length != 0)
        {
            return sortDescriptors.Aggregate(specificationBuilder, Sort);
        }

        return Sort(specificationBuilder, new SortDescriptor(nameof(AddressFilter.Id)));
    }

    private ISpecificationBuilder<Address> Sort(ISpecificationBuilder<Address> specificationBuilder,
        SortDescriptor sort)
    {
        if (SortExpressions.TryGetValue(sort.Field, out var se))
        {
            return sort.Direction == EnumSortDirection.Desc
                ? specificationBuilder.OrderByDescending(se!)
                : specificationBuilder.OrderBy(se!);
        }
        throw new BadRequestException($"Invalid field name {sort.Field}.");
    }
}