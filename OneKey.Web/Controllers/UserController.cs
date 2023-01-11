using AutoMapper;
using OneKey.Domain.Models;
using OneKey.ServiceClient;
using OneKey.Shared;
using OneKey.Shared.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using OneKey.Web.ViewModels.User;

namespace OneKey.Web.Controllers;

public class UserController : BaseController
{
    private readonly IUserServiceClient _userServiceClient;
    private readonly IIdentityResolver _identityResolver;

    public UserController(IMapper mapper,
        ITokenResolver tokenResolver,
        IPayloadResolver payloadResolver,
        IUserServiceClient userServiceClient,
        IIdentityResolver identityResolver)
        : base(mapper,
            tokenResolver,
            payloadResolver)
    {
        _userServiceClient = userServiceClient;
        _identityResolver = identityResolver;
    }

    [HttpGet("User/Register")]
    public async Task<IActionResult> Register()
    {
        var viewModel = new RegisterUserViewModel {  };
        return View(viewModel);
    }

    [HttpPost("User/Register")]
    public async Task<IActionResult> Register(RegisterUserViewModel viewModel)
    {
        var payload = await _payloadResolver.GetPayloadAsync();

        var userDTO = _mapper.Map<RegisterUserDTO>(viewModel);

        var result = await _userServiceClient.RegisterAsync(payload, userDTO);

        var token = new JwtSecurityTokenHandler().ReadJwtToken(result.Value);

        var identity = new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "OneKey Cookie"));

        await HttpContext.SignInAsync(identity);

        return RedirectToAction("index", "home");
    }

    [HttpGet("User/Login")]
    public async Task<IActionResult> Login()
    {
        var viewModel = new LoginUserViewModel {  };
        return View(viewModel);
    }

    [HttpPost("User/Login")]
    public async Task<IActionResult> Login(LoginUserViewModel viewModel)
    {
        var payload = await _payloadResolver.GetPayloadAsync();

        var userDTO = _mapper.Map<LoginUserDTO>(viewModel);

        var result = await _userServiceClient.LoginAsync(payload, userDTO);

        var token = new JwtSecurityTokenHandler().ReadJwtToken(result.Value);

        var identity = new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "OneKey Cookie"));

        await HttpContext.SignInAsync(identity);

        return RedirectToAction("index", "home");
    }

    [HttpPost("User/Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction("index", "home");
    }    
}
