using OneKey.Domain.Models;
using OneKey.Shared.Models;
using OneKey.Shared.Utilities;
using OneKey.ServiceClient;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using OneKey.Domain.Models;

namespace OneKey.ServiceClient;

public class UserServiceClient : BaseServiceClient<UserDTO>, IUserServiceClient
{
    public UserServiceClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public async Task<Result<string>> RegisterAsync(HttpPayload payload, RegisterUserDTO dto)
    {
        var client = await GetClientAsync(payload);

        var postbody = JsonConvert.SerializeObject(dto);

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var result = await client.PostAsync(@$"{AdaptiveUrl}/Register", new StringContent(postbody, Encoding.UTF8, "application/json"));

        var json = await result.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<Result<string>>(json);
    }

    public async Task<Result<string>> LoginAsync(HttpPayload payload, LoginUserDTO dto)
    {
        var client = await GetClientAsync(payload);

        var postbody = JsonConvert.SerializeObject(dto);

        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var result = await client.PostAsync(@$"{AdaptiveUrl}/Login", new StringContent(postbody, Encoding.UTF8, "application/json"));

        var json = await result.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<Result<string>>(json);
    }
}

public interface IUserServiceClient : IBaseServiceClient<UserDTO>
{
    public Task<Result<string>> RegisterAsync(HttpPayload payload, RegisterUserDTO dto);
    public Task<Result<string>> LoginAsync(HttpPayload payload, LoginUserDTO dto);
}
