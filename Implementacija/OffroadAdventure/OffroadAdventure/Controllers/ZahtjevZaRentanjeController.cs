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
    public class ZahtjevZaRentanjeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZahtjevZaRentanjeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ZahtjevZaRentanje
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ZahtjevZaRentanje.Include(z => z.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ZahtjevZaRentanje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zahtjevZaRentanje = await _context.ZahtjevZaRentanje
                .Include(z => z.User)
                .FirstOrDefaultAsync(m => m.id == id);
            if (zahtjevZaRentanje == null)
            {
                return NotFound();
            }

            return View(zahtjevZaRentanje);
        }

        // GET: ZahtjevZaRentanje/Create
        public IActionResult Create()
        {
            ViewData["korisnik_id"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ZahtjevZaRentanje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,korisnik_id,datumOd,datumDo,brojVozila,status,statusZahtjeva,popust,cijena,ime,prezime,email,brojTelefona,dodatniZahtjev")] ZahtjevZaRentanje zahtjevZaRentanje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zahtjevZaRentanje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["korisnik_id"] = new SelectList(_context.Users, "Id", "Id", zahtjevZaRentanje.korisnik_id);
            return View(zahtjevZaRentanje);
        }

        // GET: ZahtjevZaRentanje/Edit/5
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
            ViewData["korisnik_id"] = new SelectList(_context.Users, "Id", "Id", zahtjevZaRentanje.korisnik_id);
            return View(zahtjevZaRentanje);
        }

        // POST: ZahtjevZaRentanje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,korisnik_id,datumOd,datumDo,brojVozila,status,statusZahtjeva,popust,cijena,ime,prezime,email,brojTelefona,dodatniZahtjev")] ZahtjevZaRentanje zahtjevZaRentanje)
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
            ViewData["korisnik_id"] = new SelectList(_context.Users, "Id", "Id", zahtjevZaRentanje.korisnik_id);
            return View(zahtjevZaRentanje);
        }

        // GET: ZahtjevZaRentanje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zahtjevZaRentanje = await _context.ZahtjevZaRentanje
                .Include(z => z.User)
                .FirstOrDefaultAsync(m => m.id == id);
            if (zahtjevZaRentanje == null)
            {
                return NotFound();
            }

            return View(zahtjevZaRentanje);
        }

        // POST: ZahtjevZaRentanje/Delete/5
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
