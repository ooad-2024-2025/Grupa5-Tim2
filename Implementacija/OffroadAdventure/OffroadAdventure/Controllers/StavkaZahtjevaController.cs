using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OffroadAdventure.Models;

namespace OffroadAdventure.Controllers
{
    public class StavkaZahtjevaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StavkaZahtjevaController(ApplicationDbContext context)
        {
            _context = context;
        }

    
        // POST: StavkaZahtjeva/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ZahtjevZaRentanjeId,VoziloId")] StavkaZahtjeva stavkaZahtjeva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stavkaZahtjeva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VoziloId"] = new SelectList(_context.Vozilo, "id", "model", stavkaZahtjeva.VoziloId);
            ViewData["ZahtjevZaRentanjeId"] = new SelectList(_context.ZahtjevZaRentanje, "id", "id", stavkaZahtjeva.ZahtjevZaRentanjeId);
            return View(stavkaZahtjeva);
        }

      

        // POST: StavkaZahtjeva/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ZahtjevZaRentanjeId,VoziloId")] StavkaZahtjeva stavkaZahtjeva)
        {
            if (id != stavkaZahtjeva.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stavkaZahtjeva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StavkaZahtjevaExists(stavkaZahtjeva.id))
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
            ViewData["VoziloId"] = new SelectList(_context.Vozilo, "id", "model", stavkaZahtjeva.VoziloId);
            ViewData["ZahtjevZaRentanjeId"] = new SelectList(_context.ZahtjevZaRentanje, "id", "id", stavkaZahtjeva.ZahtjevZaRentanjeId);
            return View(stavkaZahtjeva);
        }

        // GET: StavkaZahtjeva/Delete/5
     
        // POST: StavkaZahtjeva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stavkaZahtjeva = await _context.StavkaZahtjeva.FindAsync(id);
            if (stavkaZahtjeva != null)
            {
                _context.StavkaZahtjeva.Remove(stavkaZahtjeva);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StavkaZahtjevaExists(int id)
        {
            return _context.StavkaZahtjeva.Any(e => e.id == id);
        }
    }
}
