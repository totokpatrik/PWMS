using PWMS.Domain.Auth.Entities;

namespace PWMS.Application.Auth.Repositories;

public interface IAuthRepository : IRepositoryBase<User>
{
    Task<Token> Register(string username, string password);
    Task<Token> Login(string username, string password);

}
