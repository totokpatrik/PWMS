namespace PWMS.Domain.Common;

public interface IBaseAuditableWarehouseEntity<TKey> : IEntity
{
    DateTime? Created { get; set; }

    TKey? CreatedBy { get; set; }

    DateTime? Modified { get; set; }

    TKey? ModifiedBy { get; set; }
    Guid? WarehouseId { get; set; }
}
