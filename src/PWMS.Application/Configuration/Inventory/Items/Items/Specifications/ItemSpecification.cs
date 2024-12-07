using PWMS.Application.Common.Paging;
using PWMS.Domain.Configuration.Inventory.Items.Entities;
using System.Collections.Frozen;
using System.Linq.Expressions;

namespace PWMS.Application.Configuration.Inventory.Items.Items.Specifications;

internal sealed class ItemSpecification : Specification<Item>
{
    private static readonly FrozenDictionary<string, Expression<Func<Item, object>>> SortExpressions =
        new Dictionary<string, Expression<Func<Item, object>>>
        {
            { nameof(Item.Id), c => c.Id },
            { nameof(Item.Name), c => c.Name },
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

    private ItemSpecification()
    {
    }

    public static Specification<Item> Create(IPageContext pageContext)
    {
        var specification = new ItemSpecification();

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

    private ISpecificationBuilder<Item> Sort(ISpecificationBuilder<Item> specificationBuilder,
        IEnumerable<SortDescriptor> sorts)
    {
        var sortDescriptors = sorts as SortDescriptor[] ?? sorts.ToArray();

        if (sortDescriptors.Length != 0)
        {
            return sortDescriptors.Aggregate(specificationBuilder, Sort);
        }

        return Sort(specificationBuilder, new SortDescriptor(nameof(Item.Id)));
    }

    private ISpecificationBuilder<Item> Sort(ISpecificationBuilder<Item> specificationBuilder,
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