using AutoMapper;
using OneKey.Entity;
using OneKey.Shared.Models;
using OneKey.Shared.Utilities;
using OneKey.Entity.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace OneKey.Repository;

public class UserRepository : BaseRepository<User, IUserRepository>, IUserRepository
{
    public UserRepository(AppDbContext context,
        ILogger<UserRepository> logger,
        IMapper mapper)
        : base(context,
              logger,
              mapper)
    {
    }
}

public interface IUserRepository : IBaseRepository<User>
{

}


