using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nge.Web.Data;
using Nge.Web.Models;

namespace Nge.Web.Services
{
    public class CodesService
    {
        private readonly ApplicationDbContext _dbContext;

        public CodesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCode(string codeValue, string codeType)
        {
            var code = new Code
            {
                Id = Guid.NewGuid(),
                Value = codeValue,
                Type = codeType,
                Created = DateTimeOffset.Now
            };

            _dbContext.Add(code);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Code>> GetAll()
        {
            return await _dbContext.Codes.ToListAsync();
        }

        public async Task<Code> Get(Guid id)
        {
            return await _dbContext.Codes.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateCode(Guid id, string value, string type)
        {
            var code = await Get(id);
            code.Type = type;
            code.Value = value;
            _dbContext.Update(code);
            await _dbContext.SaveChangesAsync();
        }

        public bool Exists(Guid id)
        {
            return _dbContext.Codes.Any(e => e.Id == id);
        }

        public async Task Remove(Guid id)
        {
            var code = await _dbContext.Codes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (code != null)
            {
                _dbContext.Codes.Remove(code);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
