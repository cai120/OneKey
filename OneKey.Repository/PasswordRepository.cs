using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneKey.Entity;
using OneKey.Entity.Models;
using OneKey.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneKey.Repository
{
    public class PasswordRepository : BaseRepository<Password, IPasswordRepository>, IPasswordRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PasswordRepository(AppDbContext context, 
            ILogger<IPasswordRepository> logger, 
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(context, logger, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<Result<List<Password>>> GetAllAsync()
        {
            var result = new Result<List<Password>> { };
            var currentUserClaims = _httpContextAccessor.HttpContext.User.Claims.ToList();

            var userExtRef = currentUserClaims.FirstOrDefault(a => a.Type == "Reference");

            try
            {
                if (Includes == null || Includes.Count == 0)
                {
                    result.Value = await Table.ToListAsync();
                }
                else
                {
                    var entities = Table.AsQueryable();
                    foreach (var include in Includes)
                        entities = entities.Include(include);

                    result.Value = await entities.Where(a=>a.UserReference == userExtRef.Value).ToListAsync();
                }

                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                result.Success = false;
                result.Message = $"Exception Thrown. Error: {ex.Message}";
            }

            return result;
        }

        public override async Task<Result<Password>> InsertAsync(Password entity)
        {
            var result = new Result<Password> { };

            var currentUserClaims = _httpContextAccessor.HttpContext.User.Claims.ToList();

            var userExtRef = currentUserClaims.FirstOrDefault(a => a.Type == "Reference");

            entity.UserReference = userExtRef.Value;

            result.Value = entity;
            try
            {
                if (string.IsNullOrWhiteSpace(entity.Reference))
                    entity.Reference = Guid.NewGuid().ToString();

                await Table.AddAsync(entity);
                Context.Entry(entity).State = EntityState.Added;
                await SaveChangesAsync();
                result.Message = entity.Reference;
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                result.Success = false;
                result.Message = $"Exception Thrown. Error: {ex.Message}";
            }

            return result;
        }

        public override async Task<Result<Password>> UpdateAsync(Password entity)
        {
            var result = new Result<Password> { };

            var currentUserClaims = _httpContextAccessor.HttpContext.User.Claims.ToList();

            var userExtRef = currentUserClaims.FirstOrDefault(a => a.Type == "Reference");
            if(entity.UserReference != userExtRef.Value)
                entity.UserReference = userExtRef.Value;

            result.Value = entity;
            try
            {
                Context.Attach(entity);
                Context.Update(entity).State = EntityState.Modified;
                await SaveChangesAsync();
                result.Message = entity.Reference;
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                result.Success = false;
                result.Message = $"Exception Thrown. Error: {ex.Message}";
            }

            return result;
        }


    }
    public interface IPasswordRepository : IBaseRepository<Password>
    {

    }
}
