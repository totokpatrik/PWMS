namespace PWMS.Domain.Common;

public abstract class BaseAuditableWarehouseEntity<TPKey, TUserPKey> : BaseEntity<TPKey>, IBaseAuditableWarehouseEntity<TUserPKey>
{
    public DateTime? Created { get; set; }

    public TUserPKey? CreatedBy { get; set; }

    public DateTime? Modified { get; set; }

    public TUserPKey? ModifiedBy { get; set; }
    public Guid? WarehouseId { get; set; }

    protected BaseAuditableWarehouseEntity(TPKey id) : base(id)
    {
    }
}
