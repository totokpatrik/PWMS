using PWMS.Domain.Core.Warehouses.Entities;

namespace PWMS.Domain.Common;

public abstract class BaseAuditableEntity<TPKey, TUserPKey> : BaseEntity<TPKey>, IBaseAuditableEntity<TUserPKey>
{
    public DateTime? Created { get; set; }

    public TUserPKey? CreatedBy { get; set; }

    public DateTime? Modified { get; set; }

    public TUserPKey? ModifiedBy { get; set; }
    public Warehouse? Warehouse { get; set; }
    public Guid? WarehouseId { get; set; }

    protected BaseAuditableEntity(TPKey id) : base(id)
    {
    }
}
