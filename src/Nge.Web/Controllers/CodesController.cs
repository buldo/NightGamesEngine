using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nge.Web.Data;
using Nge.Web.Models;
using Nge.Web.Models.Codes;

namespace Nge.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class CodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CodesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Codes
        public async Task<IActionResult> Index()
        {
            var vm = new IndexViewModel()
            {
                Codes = await _context.Codes.ToListAsync()
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

            var code = await _context.Codes
                .SingleOrDefaultAsync(m => m.Id == id);
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
                var code = new Code
                {
                    Id = Guid.NewGuid(),
                    Value = newCodeValue.Trim(),
                    Type = newCodeType.Trim()
                };
                _context.Add(code);
                await _context.SaveChangesAsync();
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

            var code = await _context.Codes.SingleOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Type,Value")] Code code)
        {
            if (id != code.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(code);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CodeExists(code.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(code);
        }

        // GET: Codes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id != null)
            {
                var code = await _context.Codes
                    .SingleOrDefaultAsync(m => m.Id == id);
                if (code != null)
                {
                    _context.Codes.Remove(code);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CodeExists(Guid id)
        {
            return _context.Codes.Any(e => e.Id == id);
        }
    }
}
