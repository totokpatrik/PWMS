using PWMS.Application.Common.Paging;
using PWMS.Domain.Configuration.Inventory.Items.Entities;
using System.Collections.Frozen;
using System.Linq.Expressions;

namespace PWMS.Application.Configurations.Inventory.Items.FootprintDetails.Specifications;

internal sealed class FootprintDetailSpecification : Specification<FootprintDetail>
{
    private static readonly FrozenDictionary<string, Expression<Func<FootprintDetail, object>>> SortExpressions =
        new Dictionary<string, Expression<Func<FootprintDetail, object>>>
        {
    { nameof(FootprintDetail.Id), c => c.Id },
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

    private FootprintDetailSpecification()
    {
    }

    public static Specification<FootprintDetail> Create(IPageContext pageContext)
    {
        var specification = new FootprintDetailSpecification();

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

    private ISpecificationBuilder<FootprintDetail> Sort(ISpecificationBuilder<FootprintDetail> specificationBuilder,
        IEnumerable<SortDescriptor> sorts)
    {
        var sortDescriptors = sorts as SortDescriptor[] ?? sorts.ToArray();

        if (sortDescriptors.Length != 0)
        {
            return sortDescriptors.Aggregate(specificationBuilder, Sort);
        }

        return Sort(specificationBuilder, new SortDescriptor(nameof(FootprintDetail.Id)));
    }

    private ISpecificationBuilder<FootprintDetail> Sort(ISpecificationBuilder<FootprintDetail> specificationBuilder,
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