using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OffroadAdventure.Models;
using OffroadAdventure.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OffroadAdventure.Controllers
{
    public class PlacanjeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlacanjeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Placanje
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Placanje.Include(p => p.zahtjevZaRentanje);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Placanje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placanje = await _context.Placanje
                .Include(p => p.zahtjevZaRentanje)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placanje == null)
            {
                return NotFound();
            }

            return View(placanje);
        }

        // GET: Placanje/Create
        public IActionResult Create()
        {
            ViewData["zahtjevZaRentanjeId"] = new SelectList(_context.ZahtjevZaRentanje, "id", "id");
            return View();
        }

        // POST: Placanje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,zahtjevZaRentanjeId,nacinPlacanja,status,datumPlacanja,popust")] Placanje placanje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(placanje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["zahtjevZaRentanjeId"] = new SelectList(_context.ZahtjevZaRentanje, "id", "id", placanje.zahtjevZaRentanjeId);
            return View(placanje);
        }

        // GET: Placanje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placanje = await _context.Placanje.FindAsync(id);
            if (placanje == null)
            {
                return NotFound();
            }
            ViewData["zahtjevZaRentanjeId"] = new SelectList(_context.ZahtjevZaRentanje, "id", "id", placanje.zahtjevZaRentanjeId);
            return View(placanje);
        }

        // POST: Placanje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,zahtjevZaRentanjeId,nacinPlacanja,status,datumPlacanja,popust")] Placanje placanje)
        {
            if (id != placanje.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(placanje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlacanjeExists(placanje.Id))
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
            ViewData["zahtjevZaRentanjeId"] = new SelectList(_context.ZahtjevZaRentanje, "id", "id", placanje.zahtjevZaRentanjeId);
            return View(placanje);
        }

        // GET: Placanje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placanje = await _context.Placanje
                .Include(p => p.zahtjevZaRentanje)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placanje == null)
            {
                return NotFound();
            }

            return View(placanje);
        }

        // POST: Placanje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var placanje = await _context.Placanje.FindAsync(id);
            if (placanje != null)
            {
                _context.Placanje.Remove(placanje);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> platiKarticom(int zahtjevId)
        {
            var placanje = _context.Placanje.FirstOrDefault(p => p.zahtjevZaRentanjeId == zahtjevId);
            if (placanje == null)
                return NotFound();

            placanje.nacinPlacanja = NacinPlacanja.KARTICA;
            placanje.statusPlacanja = StatusPlacanja.PLACENO;
            placanje.datumPlacanja = DateTime.Now;
            placanje.status = StatusPlacanja.PLACENO;


            var zahtjev = await _context.ZahtjevZaRentanje.FindAsync(zahtjevId);
            if (zahtjev == null)
                return NotFound();

            var uloge = new[] { "Administrator", "Zaposlenik" };

            var korisnici = await _context.Users
                .Where(u => _context.UserRoles
                    .Any(ur => ur.UserId == u.Id &&
                               _context.Roles
                                   .Any(r => r.Id == ur.RoleId && uloge.Contains(r.Name))))
                .ToListAsync();

            foreach (var k in korisnici)
            {
                _context.Notifikacija.Add(new Notifikacija
                {
                    primalac_id = k.Id,
                    tekst = $"Korisnik {zahtjev.ime} {zahtjev.prezime} je izvršio kartično plaćanje za rezervaciju vozila.",
                    datum = DateTime.Now,
                    status = StatusNotifikacije.NEPROCITANA
                });
            }

            await _context.SaveChangesAsync();

            TempData["poruka"] = $"Zahtjev je uspješno kreiran i uplata je izvršena! Očekujte obavještenje uskoro";
            return RedirectToAction("Rezervacija", "Vozilo", new { id = zahtjevId });
        }


        [HttpGet]
        public StatusPlacanja dajStatusPlacanja(int id)
        {
            var placanje = _context.Placanje.FirstOrDefault(p => p.zahtjevZaRentanjeId == id);
            if (placanje == null) NotFound();

            return placanje.statusPlacanja;
        }
        private bool PlacanjeExists(int id)
        {
            return _context.Placanje.Any(e => e.Id == id);
        }
    }
}
