using AutoMapper;
using OneKey.Service;
using OneKey.Shared.Models;
using OneKey.Domain.Models;
using OneKey.Shared.Utilities;
using OneKey.Shared.Helpers;
using OneKey.Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OneKey.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : BaseApiController<User, UserDTO, IUserService, UserController>
{
    private readonly ITokenResolver _tokenResolver;
    private readonly UserManager<User> _userManager;


    public UserController(IUserService service,
            IMapper mapper,
            ILogger<UserController> logger,
            ITokenResolver tokenResolver,
            UserManager<User> userManager)
            : base(service,
                  mapper,
                  logger)
    {
        _tokenResolver = tokenResolver;
        _userManager = userManager;
    }


    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO userDTO)
    {
        if(string.IsNullOrWhiteSpace(userDTO.Password) || string.IsNullOrWhiteSpace(userDTO.Password))
            return Ok(new Result<string> { Success = false, Message = "No valid password submitted." });
        
        if(userDTO.Password != userDTO.ConfirmPassword)
            return Ok(new Result<string> { Success = false, Message = "Passwords do not match." });
        
        var user = _mapper.Map<User>(userDTO);

        user.NormalizedEmail = userDTO.Email?.ToUpperInvariant();
        user.UserName = userDTO.Username?.ToUpperInvariant();

        var result = await _userManager.CreateAsync(user, userDTO.Password);
        
        if(!result.Succeeded)
            return Ok(new Result<string> { Success = false, Message = result?.Errors?.FirstOrDefault()?.Description ?? "" });
        
        var claimsIdentity = new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("UserName", user.UserName),
            new Claim("Email", user.Email),
            new Claim("Reference", user.Reference),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
        }, "OneKey Cookie");

        var token = await _tokenResolver.GetTokenAsync(claims: new ClaimsPrincipal(claimsIdentity));

        return Ok(new Result<string> { Success = true, Value = token });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
    {
        var result = new Result<string> { };

        var user = await Service.GetSingleWhereAsync(a => a.NormalizedUserName == userDTO.Username.ToUpper());

        if (user == null)
            return Ok(new Result<string> { Success = false, Message = "No user found." });
        
        var canSignIn = await _userManager.CheckPasswordAsync(user, userDTO.Password);

        if(!canSignIn)
            return Ok(new Result<string> { Success = false, Message = "Invalid Password." });
        
        var claimsIdentity = new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("UserName", user.UserName),
            new Claim("Email", user.Email),
            new Claim("Reference", user.Reference),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
        }, "OneKey Cookie");

        var token = await _tokenResolver.GetTokenAsync(claims: new ClaimsPrincipal(claimsIdentity));

        return Ok(new Result<string> { Success = true, Value = token });
    }

}
