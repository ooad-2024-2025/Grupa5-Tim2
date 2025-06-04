using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OffroadAdventure.Models;

namespace OffroadAdventure.Controllers
{
    public class ZahtjevZaRentanjesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZahtjevZaRentanjesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ZahtjevZaRentanjes
        [Authorize(Roles = "Administrator,Zaposlenik")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ZahtjevZaRentanje.ToListAsync());
        }

        // GET: ZahtjevZaRentanjes/Details/5
        [Authorize(Roles = "Administrator,Zaposlenik")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zahtjevZaRentanje = await _context.ZahtjevZaRentanje
                .FirstOrDefaultAsync(m => m.id == id);
            if (zahtjevZaRentanje == null)
            {
                return NotFound();
            }

            return View(zahtjevZaRentanje);
        }

        // GET: ZahtjevZaRentanjes/Create
        [Authorize(Roles = "Administrator,Zaposlenik")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ZahtjevZaRentanjes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,korisnik_id,datumOd,datumDo,brojVozila,status,popust,vrijemeTrajanja")] ZahtjevZaRentanje zahtjevZaRentanje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zahtjevZaRentanje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zahtjevZaRentanje);
        }

        // GET: ZahtjevZaRentanjes/Edit/5
        [Authorize(Roles = "Administrator,Zaposlenik")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zahtjevZaRentanje = await _context.ZahtjevZaRentanje.FindAsync(id);
            if (zahtjevZaRentanje == null)
            {
                return NotFound();
            }
            return View(zahtjevZaRentanje);
        }

        // POST: ZahtjevZaRentanjes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,korisnik_id,datumOd,datumDo,brojVozila,status,popust,vrijemeTrajanja")] ZahtjevZaRentanje zahtjevZaRentanje)
        {
            if (id != zahtjevZaRentanje.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zahtjevZaRentanje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZahtjevZaRentanjeExists(zahtjevZaRentanje.id))
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
            return View(zahtjevZaRentanje);
        }

        // GET: ZahtjevZaRentanjes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zahtjevZaRentanje = await _context.ZahtjevZaRentanje
                .FirstOrDefaultAsync(m => m.id == id);
            if (zahtjevZaRentanje == null)
            {
                return NotFound();
            }

            return View(zahtjevZaRentanje);
        }

        // POST: ZahtjevZaRentanjes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zahtjevZaRentanje = await _context.ZahtjevZaRentanje.FindAsync(id);
            if (zahtjevZaRentanje != null)
            {
                _context.ZahtjevZaRentanje.Remove(zahtjevZaRentanje);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZahtjevZaRentanjeExists(int id)
        {
            return _context.ZahtjevZaRentanje.Any(e => e.id == id);
        }
    }
}
