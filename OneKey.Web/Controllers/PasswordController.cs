using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OneKey.ServiceClient;
using OneKey.Shared.Utilities;
using OneKey.Web.ViewModels;

namespace OneKey.Web.Controllers
{
    public class PasswordController : BaseController
    {
        private readonly IPasswordServiceClient _passwordServiceClient;

        public PasswordController(IMapper mapper,
        ITokenResolver tokenResolver,
        IPayloadResolver payloadResolver,
        IPasswordServiceClient passwordServiceClient):base(mapper, tokenResolver, payloadResolver)
        {
            _passwordServiceClient = passwordServiceClient;
        }
        public async Task<IActionResult> Index()
        {
            var payload = await _payloadResolver.GetPayloadAsync();
            var allPasswords = await _passwordServiceClient.GetAllAsync(payload);

            return View(_mapper.Map<PasswordViewModel>(allPasswords));
        }
    }
}
