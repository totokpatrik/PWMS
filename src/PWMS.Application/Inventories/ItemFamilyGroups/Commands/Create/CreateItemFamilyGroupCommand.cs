using PWMS.Domain.Addresses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWMS.Application.Inventories.ItemFamilyGroups.Commands.Create;

public sealed record CreateItemFamilyGroupCommand(string Name, string Description) : IRequest<Result<Guid>>;