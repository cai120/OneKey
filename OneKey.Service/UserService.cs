using OneKey.Shared.Utilities;
using OneKey.Shared.Models;
using OneKey.Entity;
using OneKey.Repository;
using OneKey.Entity.Models;
using System;
using System.Threading.Tasks;

namespace OneKey.Service;

public class UserService : BaseService<User, IUserRepository>, IUserService
{
    public UserService(IUserRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }
}

public interface IUserService : IBaseService<User>
{

}

