using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nge.Web.Data;
using Nge.Web.Models;

namespace Nge.Web.Controllers
{
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
            return View(await _context.Codes.ToListAsync());
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

        // GET: Codes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Codes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Value")] Code code)
        {
            if (ModelState.IsValid)
            {
                code.Id = Guid.NewGuid();
                _context.Add(code);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(code);
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

        // POST: Codes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var code = await _context.Codes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Codes.Remove(code);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CodeExists(Guid id)
        {
            return _context.Codes.Any(e => e.Id == id);
        }
    }
}
