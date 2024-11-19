using PWMS.Domain.Core.Warehouses.Entities;

namespace PWMS.Domain.Common;

public interface IBaseAuditableEntity<TKey> : IEntity
{
    DateTime? Created { get; set; }

    TKey? CreatedBy { get; set; }

    DateTime? Modified { get; set; }

    TKey? ModifiedBy { get; set; }
    Warehouse? Warehouse { get; set; }
    public Guid? WarehouseId { get; set; }
}
