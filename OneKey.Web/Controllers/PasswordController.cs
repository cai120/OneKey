using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneKey.ServiceClient;
using OneKey.Shared.Utilities;
using OneKey.Web.ViewModels;
using System.Text.Json;

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
            var viewModel = _mapper.Map<List<PasswordViewModel>>(allPasswords.Value);
            return View(viewModel);
        }

        public async Task<IActionResult> Filter()
        {
            var payload = await _payloadResolver.GetPayloadAsync();
            var allPasswords = _passwordServiceClient.GetAllAsync(payload);

            var data = JsonConvert.SerializeObject(allPasswords);

            return Json(data);
        }
    }
}
