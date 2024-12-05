using PWMS.Application.Common.Paging;
using PWMS.Domain.Inventories.Items.Entities;
using System.Collections.Frozen;
using System.Linq.Expressions;

namespace PWMS.Application.Configuration.Inventory.Items.ItemFamilyGroups.Specifications;

internal sealed class ItemFamilyGroupSpecification : Specification<ItemFamilyGroup>
{
    private static readonly FrozenDictionary<string, Expression<Func<ItemFamilyGroup, object>>> SortExpressions =
        new Dictionary<string, Expression<Func<ItemFamilyGroup, object>>>
        {
            { nameof(ItemFamilyGroup.Id), c => c.Id },
            { nameof(ItemFamilyGroup.Name), c => c.Name },
            { nameof(ItemFamilyGroup.Description), c => c.Description },
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

    private ItemFamilyGroupSpecification()
    {
    }

    public static Specification<ItemFamilyGroup> Create(IPageContext pageContext)
    {
        var specification = new ItemFamilyGroupSpecification();

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

    private ISpecificationBuilder<ItemFamilyGroup> Sort(ISpecificationBuilder<ItemFamilyGroup> specificationBuilder,
        IEnumerable<SortDescriptor> sorts)
    {
        var sortDescriptors = sorts as SortDescriptor[] ?? sorts.ToArray();

        if (sortDescriptors.Length != 0)
        {
            return sortDescriptors.Aggregate(specificationBuilder, Sort);
        }

        return Sort(specificationBuilder, new SortDescriptor(nameof(ItemFamilyGroup.Id)));
    }

    private ISpecificationBuilder<ItemFamilyGroup> Sort(ISpecificationBuilder<ItemFamilyGroup> specificationBuilder,
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