using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nge.Web.Data;
using Nge.Web.Models;
using Nge.Web.Repos;

namespace Nge.Web.Controllers
{
    [Authorize]
    public class PlayController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CodesRepo _codesRepo;

        public PlayController(
            UserManager<ApplicationUser> userManager,
            CodesRepo codesRepo)
        {
            _userManager = userManager;
            _codesRepo = codesRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EnterCode(string codeToEnter)
        {
            var user = await _userManager.GetUserAsync(User);
            await _codesRepo.EnterCode(codeToEnter, user);
            return RedirectToAction("Index");
        }
    }
}