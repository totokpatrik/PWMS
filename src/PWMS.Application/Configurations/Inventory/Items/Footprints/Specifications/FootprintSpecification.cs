using PWMS.Application.Common.Paging;
using PWMS.Domain.Configuration.Inventory.Items.Entities;
using System.Collections.Frozen;
using System.Linq.Expressions;

namespace PWMS.Application.Configurations.Inventory.Items.Footprints.Specifications;

internal sealed class FootprintSpecification : Specification<Footprint>
{
    private static readonly FrozenDictionary<string, Expression<Func<Footprint, object>>> SortExpressions =
        new Dictionary<string, Expression<Func<Footprint, object>>>
        {
        { nameof(Footprint.Id), c => c.Id },
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

    private FootprintSpecification()
    {
    }

    public static Specification<Footprint> Create(IPageContext pageContext)
    {
        var specification = new FootprintSpecification();

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
            .AsNoTracking();
        return specification;
    }

    private ISpecificationBuilder<Footprint> Sort(ISpecificationBuilder<Footprint> specificationBuilder,
        IEnumerable<SortDescriptor> sorts)
    {
        var sortDescriptors = sorts as SortDescriptor[] ?? sorts.ToArray();

        if (sortDescriptors.Length != 0)
        {
            return sortDescriptors.Aggregate(specificationBuilder, Sort);
        }

        return Sort(specificationBuilder, new SortDescriptor(nameof(Footprint.Id)));
    }

    private ISpecificationBuilder<Footprint> Sort(ISpecificationBuilder<Footprint> specificationBuilder,
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