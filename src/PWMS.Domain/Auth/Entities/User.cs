using Microsoft.AspNetCore.Identity;
using PWMS.Domain.Common;

namespace PWMS.Domain.Auth.Entities;

public class User : IdentityUser, IAggregateRoot
{
}
