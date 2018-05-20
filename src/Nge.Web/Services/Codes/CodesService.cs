using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nge.Web.Data;
using Nge.Web.Models;

namespace Nge.Web.Services.Codes
{
    public class CodesService
    {
        private readonly ApplicationDbContext _dbContext;

        public CodesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CodeUpdateResult> AddCodeAsync(string codeValue, string codeType)
        {
            if (Exists(codeValue))
            {
                return CodeUpdateResult.CreateExisted();
            }

            var code = new Code
            {
                Id = Guid.NewGuid(),
                Value = codeValue,
                Type = codeType,
                Created = DateTimeOffset.Now
            };

            _dbContext.Add(code);
            await _dbContext.SaveChangesAsync();
            return CodeUpdateResult.CreateSuccess();
        }

        public async Task<List<Code>> GetAllAsync()
        {
            return await _dbContext.Codes.ToListAsync();
        }

        public async Task<Code> GetAsync(Guid id)
        {
            return await _dbContext.Codes.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<CodeUpdateResult> UpdateCodeAsync(Guid id, string value, string type)
        {
            if (Exists(value))
            {
                return CodeUpdateResult.CreateExisted();
            }

            var code = await GetAsync(id);
            code.Type = type;
            code.Value = value;
            _dbContext.Update(code);
            await _dbContext.SaveChangesAsync();
            return CodeUpdateResult.CreateSuccess();
        }

        public bool Exists(Guid id)
        {
            return _dbContext.Codes.Any(e => e.Id == id);
        }

        public async Task RemoveAsync(Guid id)
        {
            var code = await _dbContext.Codes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (code != null)
            {
                _dbContext.Codes.Remove(code);
                await _dbContext.SaveChangesAsync();
            }
        }

        private bool Exists(string value)
        {
            return _dbContext.Codes.Any(e => e.Value == value);
        }
    }
}
