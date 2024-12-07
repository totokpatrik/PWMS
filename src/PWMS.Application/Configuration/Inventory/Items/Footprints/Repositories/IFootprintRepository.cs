﻿using Castle.DynamicLinqQueryBuilder;
using PWMS.Domain.Configuration.Inventory.Items.Entities;

namespace PWMS.Application.Configuration.Inventory.Items.Footprints.Repositories;
public interface IFootprintRepository : IRepositoryBase<Footprint>
{
    Task<List<Footprint>> GetAllFootprints(
        ISpecification<Footprint> specification,
        CancellationToken cancellationToken,
        QueryBuilderFilterRule filter);
}
