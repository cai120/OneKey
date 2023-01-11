using OneKey.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneKey.ServiceClient
{
    public class PasswordServiceClient : BaseServiceClient<PasswordDTO>, IPasswordServiceClient
    {
        public PasswordServiceClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }
    }

    public interface IPasswordServiceClient: IBaseServiceClient<PasswordDTO>
    {

    }
}
