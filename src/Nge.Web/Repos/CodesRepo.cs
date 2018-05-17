using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nge.Web.Data;
using Nge.Web.Models;

namespace Nge.Web.Repos
{
    public class CodesRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public CodesRepo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task EnterCode(string code, ApplicationUser user)
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
    }
}
