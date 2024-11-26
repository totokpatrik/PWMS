using PWMS.Domain.Auth.Entities;
using PWMS.Domain.Core.Sites.Entities;
using PWMS.Domain.Core.Warehouses.Entities;

namespace PWMS.Application.Auth.Repositories;

public interface IAuthRepository : IRepositoryBase<User>
{
    Task<Token> Register(string username, string password);
    Task<Token> Login(string username, string password);
    Task<Token> BuildToken(string username);
    Task SelectSite(Site site, string username);
    Task SelectWarehouse(Warehouse warehouse, string username);
}
