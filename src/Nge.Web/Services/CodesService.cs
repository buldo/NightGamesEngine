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

        public async Task EnterCodeAsync(string code, ApplicationUser user)
        {
            var enterCodeEvent = new EnterCodeEvent()
            {
                User = user,
                Value = code,
                Entered = DateTimeOffset.Now
            };

            await _dbContext.EnteredCodes.AddAsync(enterCodeEvent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<HashSet<(Guid CodeId, string Type, string Value)>> GetSuccessCodesAsync(ApplicationUser user)
        {
            var goodCodes = await _dbContext
                .EnteredCodes
                .Where(c => c.User == user)
                .Join(_dbContext.Codes, e => e.Value, c => c.Value, (e, code) => new {code.Id, code.Type, code.Value})
                .ToListAsync();

            return goodCodes.Select(c => (CodeId: c.Id, Type: c.Type, Value: c.Value)).ToHashSet();
        }

        public async Task<HashSet<(Guid CodeId, string Type)>> GetLevelCodesAsync()
        {
            var codes = await _dbContext
                .Codes
                .Select(c => new {c.Id, c.Type})
                .ToListAsync();
            return codes.Select(c => (CodeId: c.Id, Type: c.Type)).ToHashSet();
        }
    }
}
