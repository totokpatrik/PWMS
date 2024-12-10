using PWMS.Application.Common.Paging;
using PWMS.Domain.Core.Sites.Entities;
using System.Collections.Frozen;
using System.Linq.Expressions;

namespace PWMS.Application.Core.Sites.Specifications;

internal sealed class SiteSpecification : Specification<Site>
{
    private static readonly FrozenDictionary<string, Expression<Func<Site, object>>> SortExpressions =
        new Dictionary<string, Expression<Func<Site, object>>>
        {
            { nameof(Site.Id), c => c.Id },
        }.ToFrozenDictionary(StringComparer.OrdinalIgnoreCase);

    private SiteSpecification()
    {
    }

    public static Specification<Site> Create(IPageContext pageContext, string currentUserId)
    {
        var specification = new SiteSpecification();

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

    private ISpecificationBuilder<Site> Sort(ISpecificationBuilder<Site> specificationBuilder,
        IEnumerable<SortDescriptor> sorts)
    {
        var sortDescriptors = sorts as SortDescriptor[] ?? sorts.ToArray();

        if (sortDescriptors.Length != 0)
        {
            return sortDescriptors.Aggregate(specificationBuilder, Sort);
        }

        return Sort(specificationBuilder, new SortDescriptor(nameof(Site.Id)));
    }

    private ISpecificationBuilder<Site> Sort(ISpecificationBuilder<Site> specificationBuilder,
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