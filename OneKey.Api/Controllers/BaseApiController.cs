using AutoMapper;
using OneKey.Shared.Models;
using OneKey.Shared.Helpers;
using OneKey.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneKey.Shared.Utilities;

[Route("[controller]")]
public class BaseApiController<TEntity, TDto, TService, TController> : Controller where TEntity : class, IBaseEntity where TService : IBaseService<TEntity> where TDto : BaseDTO
{
    protected readonly TService Service;
    protected readonly IMapper _mapper;
    protected readonly ILogger<TController> _logger;

    public BaseApiController(TService service, IMapper mapper, ILogger<TController> logger)
    {
        Service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost("GetAllAsync")]
    //[Authorize(Policy = "AccountOnly")]
    public virtual async Task<IActionResult> GetAllAsync([FromBody] PostBody body)
    {
        var result = new Result<List<TEntity>> { };

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
        catch(Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return Ok(_mapper.Map<Result<List<TDto>>>(result));
    }

    [HttpPost("GetAllWhereAsync")]
    public virtual async Task<IActionResult> GetAllWhereAsync([FromBody] PostBody body)
    {
        var result = new Result<List<TEntity>> { };

        try
        {
            if (!string.IsNullOrWhiteSpace(body.Expression))
                body.Expression = StringHelper.GenerateExpression(body.Expression);

            if (!string.IsNullOrEmpty(body.Expression) && body.Includes.Count > 0 && body.Pagination != null)
            {
                var includes = StringHelper.GenerateIncludes(body.Includes);

                result = await Service.GetAllWhereAsync(body.Expression, includes, body.Pagination);
            }

            else if (!string.IsNullOrWhiteSpace(body.Expression) && body.Includes.Count > 0)
            {
                var includes = StringHelper.GenerateIncludes(body.Includes);

                result = await Service.GetAllWhereAsync(body.Expression, includes);
            }
            else if (body.Pagination != null)
                result = await Service.GetAllWhereAsync(body.Expression, body.Pagination);

            else
                result = await Service.GetAllWhereAsync(body.Expression);
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return Ok(_mapper.Map<Result<List<TDto>>>(result));
    }

    [HttpPost("GetSingleWhereAsync")]
    public virtual async Task<IActionResult> GetSingleWhereAsync([FromBody] PostBody body)
    {
        TEntity entity = null;
        try
        {
            if (!string.IsNullOrWhiteSpace(body.Expression))
                body.Expression = StringHelper.GenerateExpression(body.Expression);

            if (!string.IsNullOrWhiteSpace(body.Expression) && body.Includes.Count > 0)
            {
                var includes = StringHelper.GenerateIncludes(body.Includes);

                entity = await Service.GetSingleWhereAsync(body.Expression, includes);
            }

            else
                entity = await Service.GetSingleWhereAsync(body.Expression);

            var dto = _mapper.Map<TDto>(entity);

            return Ok(dto);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Ok(_mapper.Map<TDto>(entity));
        }
    }

    [HttpDelete("Delete/{reference}")]
    public virtual async Task<IActionResult> DeleteAsync(string reference)
    {
        var result = await Service.DeleteAsync(reference);

        return Ok(result);
    }

    [HttpPut("Update")]
    public virtual async Task<IActionResult> UpdateAsync([FromBody] TDto entityDTO)
    {
        var result = new Result<TEntity> { };

        try
        {
            var entity = await Service.GetSingleWhereAsync(e => e.Reference == entityDTO.Reference);

            _mapper.Map(entityDTO, entity);

            result = await Service.UpdateAsync(entity);
        }
        catch(Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }
        return Ok(_mapper.Map<Result<TDto>>(result));
    }

    [HttpPost("CreateAsync")]
    public virtual async Task<IActionResult> CreateAsync([FromBody] TDto entityDTO)
    {
        var result = new Result<TEntity> { };
        try
        {
            var entity = _mapper.Map<TEntity>(entityDTO);

            result = await Service.AddAsync(entity);

            var dto = _mapper.Map<Result<TDto>>(result);

            return Ok(dto);
        }
        catch(Exception ex)
        {
            result.Success = false;
            result.Message = ex.Message;
        }

        return Ok(_mapper.Map<Result<TDto>>(result));
    }
}

