using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nge.Web.Models;
using Nge.Web.Models.Codes;
using Nge.Web.Services.Codes;

namespace Nge.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class CodesController : Controller
    {
        private readonly CodesService _codesService;

        public CodesController(CodesService codesService)
        {
            _codesService = codesService;
        }

        // GET: Codes
        public async Task<IActionResult> Index()
        {
            var vm = await CreateIndexViewModelAsync(string.Empty, string.Empty);
            return View(vm);
        }

        // GET: Codes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var code = await _codesService.GetAsync(id.Value);
            if (code == null)
            {
                return NotFound();
            }

            return View(code);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string newCodeType, string newCodeValue)
        {
            if (ModelState.IsValid)
            {
                var result = await _codesService.AddCodeAsync(
                    newCodeValue.Trim(),
                    newCodeType.Trim());
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                AddError(result.Error.Value);
            }

            var vm = await CreateIndexViewModelAsync(newCodeValue, newCodeType);
            return View(nameof(Index), vm);
        }

        // GET: Codes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!_codesService.Exists(id.Value))
            {
                return NotFound();
            }

            var code = await CreateEditViewModelAsync(id.Value);
            return View(code);
        }

        // POST: Codes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, string type, string value)
        {
            if (!_codesService.Exists(id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _codesService.UpdateCodeAsync(id, value, type);
                if (result.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }

                AddError(result.Error.Value);

                var vm = await CreateEditViewModelAsync(id, value, type);
                return View(nameof(Edit), vm);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Codes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id != null)
            {
                await _codesService.RemoveAsync(id.Value);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<IndexViewModel> CreateIndexViewModelAsync(string value, string type)
        {
            return new IndexViewModel
            {
                Codes = (await _codesService.GetAllAsync()).OrderByDescending(code => code.Created).ToList(),
                NewCodeType = type,
                NewCodeValue = value
            };
        }

        private async Task<Code> CreateEditViewModelAsync(Guid id, string value = null, string type = null)
        {
            var code = await _codesService.GetAsync(id);

            if (!string.IsNullOrWhiteSpace(value))
            {
                code.Value = value;
            }

            if (!string.IsNullOrWhiteSpace(type))
            {
                code.Type = type;
            }

            return code;
        }

        private void AddError(Error errorValue)
        {
            switch (errorValue)
            {
                case Error.CodeExists:
                    ModelState.AddModelError(string.Empty, "Код существует");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(errorValue), errorValue, null);
            }
        }
    }
}
