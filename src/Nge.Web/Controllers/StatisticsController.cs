using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nge.Web.Data;
using Nge.Web.Models;
using Nge.Web.Models.StatisticsViewModels;
using Nge.Web.Services;

namespace Nge.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly EnteredCodesService _enteredCodesService;

        public StatisticsController(
            ApplicationDbContext dbContext,
            EnteredCodesService enteredCodesService)
        {
            _dbContext = dbContext;
            _enteredCodesService = enteredCodesService;
        }

        public async Task<IActionResult> Index()
        {
            var stat= new List<PlayerViewModel>();
            var users = _dbContext.Users;
            foreach (var user in users)
            {
                var codes = await _enteredCodesService.GetSuccessCodesAsync(user);
                stat.Add(new PlayerViewModel
                {
                    Count = codes.Count,
                    Name = user.UserName
                });
            }

            return View(new IndexViewModel {Players = stat});
        }
    }
}