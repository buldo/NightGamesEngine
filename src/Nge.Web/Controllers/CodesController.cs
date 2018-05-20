using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nge.Web.Models;
using Nge.Web.Models.Codes;
using Nge.Web.Services;

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
            var vm = new IndexViewModel
            {
                Codes = (await _codesService.GetAll()).OrderByDescending(code => code.Created).ToList()
            };

            return View(vm);
        }

        // GET: Codes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var code = await _codesService.Get(id.Value);
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
                await _codesService.AddCode(
                    newCodeValue.Trim(),
                    newCodeType.Trim());
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Codes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var code = await _codesService.Get(id.Value);
            if (code == null)
            {
                return NotFound();
            }
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
                await _codesService.UpdateCode(id, value, type);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Codes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id != null)
            {
                await _codesService.Remove(id.Value);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
