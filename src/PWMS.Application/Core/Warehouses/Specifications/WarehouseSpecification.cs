using PWMS.Application.Common.Paging;
using PWMS.Domain.Core.Warehouses.Entities;
using System.Collections.Frozen;
using System.Linq.Expressions;

namespace PWMS.Application.Core.Warehouses.Specifications;

internal sealed class WarehouseSpecification : Specification<Warehouse>
{
    private static readonly FrozenDictionary<string, Expression<Func<Warehouse, object>>> SortExpressions =
        new Dictionary<string, Expression<Func<Warehouse, object>>>
        {
            { nameof(Warehouse.Id), c => c.Id },
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

    private WarehouseSpecification()
    {
    }

    public static Specification<Warehouse> Create(IPageContext pageContext, string currentUserId)
    {
        var specification = new WarehouseSpecification();

        var specificationBuilder = specification.Query;

        //Filter(specificationBuilder, pageContext.Filter);
        specification.Sort(specificationBuilder, pageContext.ListSort);

        if (pageContext.PageIndex != 0)
        {
            specificationBuilder.Skip(pageContext.PageSize * (pageContext.PageIndex - 1));
        }

        if (pageContext.PageSize != 0)
        {
            specificationBuilder.Take(pageContext.PageSize);
        }

        specificationBuilder
            .Where(s => s.Owner.Id == currentUserId || s.Admins.Any(a => a.Id == currentUserId) || s.Users.Any(u => u.Id == currentUserId));

        specificationBuilder.AsNoTracking();
        return specification;
    }

    private ISpecificationBuilder<Warehouse> Sort(ISpecificationBuilder<Warehouse> specificationBuilder,
        IEnumerable<SortDescriptor> sorts)
    {
        var sortDescriptors = sorts as SortDescriptor[] ?? sorts.ToArray();

        if (sortDescriptors.Length != 0)
        {
            return sortDescriptors.Aggregate(specificationBuilder, Sort);
        }

        return Sort(specificationBuilder, new SortDescriptor(nameof(Warehouse.Id)));
    }

    private ISpecificationBuilder<Warehouse> Sort(ISpecificationBuilder<Warehouse> specificationBuilder,
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