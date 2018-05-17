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
    public class EnterCodeEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnterCodeEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EnterCodeEvents
        public async Task<IActionResult> Index()
        {
            return View(await _context.EnteredCodes.ToListAsync());
        }

        // GET: EnterCodeEvents/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enterCodeEvent = await _context.EnteredCodes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (enterCodeEvent == null)
            {
                return NotFound();
            }

            return View(enterCodeEvent);
        }

        // GET: EnterCodeEvents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EnterCodeEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,Entered")] EnterCodeEvent enterCodeEvent)
        {
            if (ModelState.IsValid)
            {
                enterCodeEvent.Id = Guid.NewGuid();
                _context.Add(enterCodeEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enterCodeEvent);
        }

        // GET: EnterCodeEvents/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enterCodeEvent = await _context.EnteredCodes.SingleOrDefaultAsync(m => m.Id == id);
            if (enterCodeEvent == null)
            {
                return NotFound();
            }
            return View(enterCodeEvent);
        }

        // POST: EnterCodeEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Value,Entered")] EnterCodeEvent enterCodeEvent)
        {
            if (id != enterCodeEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enterCodeEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnterCodeEventExists(enterCodeEvent.Id))
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
            return View(enterCodeEvent);
        }

        // GET: EnterCodeEvents/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enterCodeEvent = await _context.EnteredCodes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (enterCodeEvent == null)
            {
                return NotFound();
            }

            return View(enterCodeEvent);
        }

        // POST: EnterCodeEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var enterCodeEvent = await _context.EnteredCodes.SingleOrDefaultAsync(m => m.Id == id);
            _context.EnteredCodes.Remove(enterCodeEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnterCodeEventExists(Guid id)
        {
            return _context.EnteredCodes.Any(e => e.Id == id);
        }
    }
}
