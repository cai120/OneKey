using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneKey.Domain;
using OneKey.Entity.Models;
using OneKey.Service;
using OneKey.Shared.Helpers;
using OneKey.Shared.Models;
using OneKey.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneKey.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordController : BaseApiController<Password, PasswordDTO, IPasswordService, PasswordController>
    {
        public PasswordController(IPasswordService service, IMapper mapper, ILogger<PasswordController> logger) : base(service, mapper, logger)
        {
        }
        [HttpPost("GetAllAsync")]
        //[Authorize(Policy = "AccountOnly")]
        public override async Task<IActionResult> GetAllAsync([FromBody] PostBody body)
        {
            var result = new Result<List<Password>> { };

            try
            {
                if (body.Includes.Count > 0 && body.Pagination != null)
                {
                    var includes = StringHelper.GenerateIncludes(body.Includes);

                    result = await Service.GetAllAsync(body.Pagination, includes);
                }
                else if (body.Includes.Count > 0)
                {
                    var includes = StringHelper.GenerateIncludes(body.Includes);

                    result = await Service.GetAllAsync(includes);
                }
                else if (body.Pagination != null)
                    result = await Service.GetAllAsync(body.Pagination);

                else
                    result = await Service.GetAllAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return Ok(_mapper.Map<Result<List<Password>>>(result));
        }
    }
}
