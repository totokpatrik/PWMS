﻿using PWMS.Domain.Common;

namespace PWMS.Domain.Inventories.Items.Entities;

public class Item : BaseAuditableWarehouseEntity<Guid, string>, IAggregateRoot
{
    public Item(string name, string description, string shortDescription, bool receiveStatus) : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
        ShortDescription = shortDescription;
        ReceiveStatus = receiveStatus;
    }

    public Item(Guid id, string name, string description, string shortDescription, bool receiveStatus) : base(id)
    {
        Name = name;
        Description = description;
        ShortDescription = shortDescription;
        ReceiveStatus = receiveStatus;
    }

    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ShortDescription { get; set; } = default!;
    public bool ReceiveStatus { get; set; } = false;
    public ItemFamily? ItemFamily { get; set; }
    public ICollection<Footprint>? Footprints { get; set; }
    public ICollection<AlternateItem>? AlternateItems { get; set; }
}