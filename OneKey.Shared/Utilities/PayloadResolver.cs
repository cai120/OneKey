using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneKey.Shared.Utilities;
public class PayloadResolver : IPayloadResolver
{
    private readonly ITokenResolver _tokenResolver;

    public PayloadResolver(ITokenResolver tokenResolver)
    {
        _tokenResolver = tokenResolver;
    }

    public async Task<HttpPayload> GetPayloadAsync()
    {
        return new HttpPayload
        {
            SecurityToken = await _tokenResolver.GetTokenAsync(null),
            Uri = OneKeyApiConstants.HostUrl
        };
    }
}

public interface IPayloadResolver
{
    public Task<HttpPayload> GetPayloadAsync();
}

