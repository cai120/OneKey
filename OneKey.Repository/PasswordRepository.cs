using AutoMapper;
using Microsoft.Extensions.Logging;
using OneKey.Entity;
using OneKey.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneKey.Repository
{
    public class PasswordRepository : BaseRepository<Password, IPasswordRepository>, IPasswordRepository
    {
        public PasswordRepository(AppDbContext context, ILogger<IPasswordRepository> logger, IMapper mapper) : base(context, logger, mapper)
        {
        }
    }
    public interface IPasswordRepository : IBaseRepository<Password>
    {

    }
}
