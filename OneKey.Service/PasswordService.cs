using OneKey.Entity.Models;
using OneKey.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneKey.Service
{
    public class PasswordService : BaseService<Password, IPasswordRepository>, IPasswordService
    {
        public PasswordService(IPasswordRepository repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }

    public interface IPasswordService:IBaseService<Password>
    {

    }
}
