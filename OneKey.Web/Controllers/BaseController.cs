using AutoMapper;
using OneKey.Domain.Models;
using OneKey.Shared;
using OneKey.Shared.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace OneKey.Web.Controllers;

public class BaseController : Controller
{
    protected readonly IMapper _mapper;
    protected readonly ITokenResolver _tokenResolver;
    protected readonly IPayloadResolver _payloadResolver;

    public BaseController(IMapper mapper,
        ITokenResolver tokenResolver,
        IPayloadResolver payloadResolver)
    {
        _mapper = mapper;
        _tokenResolver = tokenResolver;
        _payloadResolver = payloadResolver;
    }
}
