﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OneKey.Domain;
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
        IPasswordServiceClient passwordServiceClient) : base(mapper, tokenResolver, payloadResolver)
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PasswordViewModel viewModel)
        {
            var payload = await _payloadResolver.GetPayloadAsync();

            viewModel.StoredPassword = EncodePasswordToBase64(viewModel.StoredPassword);

            var result = _passwordServiceClient.CreateAsync(payload, _mapper.Map<PasswordDTO>(viewModel));
            return RedirectToAction("Index");
        }

        private static string EncodePasswordToBase64(string password)
        {

            byte[] encData_byte = new byte[password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }

        private string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public async Task<IActionResult> Filter()
        {
            var payload = await _payloadResolver.GetPayloadAsync();
            var allPasswords = _passwordServiceClient.GetAllAsync(payload);

            var data = JsonConvert.SerializeObject(allPasswords,
                                        new JsonSerializerSettings()
                                        {
                                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                        });

            return Json(data);
        }
    }
}
