using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nge.Web.Models;
using Nge.Web.Models.PlayViewModels;
using Nge.Web.Services;

namespace Nge.Web.Controllers
{
    public class PlayController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EnteredCodesService _enteredCodesService;

        public PlayController(
            UserManager<ApplicationUser> userManager,
            EnteredCodesService enteredCodesService)
        {
            _userManager = userManager;
            _enteredCodesService = enteredCodesService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var enteredCodes = await _enteredCodesService.GetSuccessCodesAsync(user);
            var allCodes = await _enteredCodesService.GetLevelCodesAsync();
            allCodes.RemoveWhere(c => enteredCodes.Any(ec => ec.CodeId == c.CodeId));

            var codesVm = new List<ShortCodeViewModel>(enteredCodes.Count + allCodes.Count);
            foreach (var enteredCode in enteredCodes)
            {
                codesVm.Add(new ShortCodeViewModel
                {
                    CodeType = enteredCode.Type,
                    IsEntered = true
                });
            }

            foreach (var code in allCodes)
            {
                codesVm.Add(new ShortCodeViewModel
                {
                    CodeType = code.Type,
                    IsEntered = false
                });
            }

            var enteredCodesVms = new List<DetailCodeViewModel>(enteredCodes.Count);
            foreach (var enteredCode in enteredCodes)
            {
                enteredCodesVms.Add(new DetailCodeViewModel
                {
                    CodeType = enteredCode.Type,
                    CodeValue = enteredCode.Value
                });
            }

            var pageViewModel = new IndexViewModel
            {
                Codes = codesVm,
                EnteredCodes = enteredCodesVms

            };

            return View(pageViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EnterCode(string codeToEnter)
        {
            codeToEnter = codeToEnter.Trim();
            var user = await _userManager.GetUserAsync(User);
            await _enteredCodesService.EnterCodeAsync(codeToEnter, user);
            return RedirectToAction("Index");
        }
    }
}