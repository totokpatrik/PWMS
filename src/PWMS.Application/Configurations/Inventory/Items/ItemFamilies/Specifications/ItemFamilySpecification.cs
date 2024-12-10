using PWMS.Application.Common.Paging;
using PWMS.Domain.Configuration.Inventory.Items.Entities;
using System.Collections.Frozen;
using System.Linq.Expressions;

namespace PWMS.Application.Configurations.Inventory.Items.ItemFamilies.Specifications;

internal sealed class ItemFamilySpecification : Specification<ItemFamily>
{
    private static readonly FrozenDictionary<string, Expression<Func<ItemFamily, object>>> SortExpressions =
        new Dictionary<string, Expression<Func<ItemFamily, object>>>
        {
            { nameof(ItemFamily.Id), c => c.Id },
            { nameof(ItemFamily.Description), c => c.Description! },
            { nameof(ItemFamily.ItemFamilyGroup), c => c.ItemFamilyGroup },
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

    private ItemFamilySpecification()
    {
    }

    public static Specification<ItemFamily> Create(IPageContext pageContext)
    {
        var specification = new ItemFamilySpecification();

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

    private ISpecificationBuilder<ItemFamily> Sort(ISpecificationBuilder<ItemFamily> specificationBuilder,
        IEnumerable<SortDescriptor> sorts)
    {
        var sortDescriptors = sorts as SortDescriptor[] ?? sorts.ToArray();

        if (sortDescriptors.Length != 0)
        {
            return sortDescriptors.Aggregate(specificationBuilder, Sort);
        }

        return Sort(specificationBuilder, new SortDescriptor(nameof(ItemFamily.Id)));
    }

    private ISpecificationBuilder<ItemFamily> Sort(ISpecificationBuilder<ItemFamily> specificationBuilder,
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