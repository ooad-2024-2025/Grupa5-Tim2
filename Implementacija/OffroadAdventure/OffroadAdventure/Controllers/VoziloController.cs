using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OffroadAdventure.Models;
using Microsoft.AspNetCore.Authorization;

namespace OffroadAdventure.Controllers
{
    public class VoziloController : Controller

    {
      
        private readonly ApplicationDbContext _context;

        public VoziloController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vozilo
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vozilo.ToListAsync());
        }

        [Authorize(Roles = "Administrator")]
        // GET: Vozilo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozilo = await _context.Vozilo
                .FirstOrDefaultAsync(m => m.id == id);
            if (vozilo == null)
            {
                return NotFound();
            }

            return View(vozilo);
        }

        // GET: Vozilo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vozilo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("id,model,tip,cijenaPoDanu,dostupno,slikaURL")] Vozilo vozilo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vozilo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vozilo);
        }

        // GET: Vozilo/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozilo = await _context.Vozilo.FindAsync(id);
            if (vozilo == null)
            {
                return NotFound();
            }
            return View(vozilo);
        }

        // POST: Vozilo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("id,model,tip,cijenaPoDanu,dostupno,slikaURL")] Vozilo vozilo)
        {
            if (id != vozilo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vozilo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoziloExists(vozilo.id))
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
            return View(vozilo);
        }

        // GET: Vozilo/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vozilo = await _context.Vozilo
                .FirstOrDefaultAsync(m => m.id == id);
            if (vozilo == null)
            {
                return NotFound();
            }

            return View(vozilo);
        }

        // POST: Vozilo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vozilo = await _context.Vozilo.FindAsync(id);
            if (vozilo != null)
            {
                _context.Vozilo.Remove(vozilo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoziloExists(int id)
        {
            return _context.Vozilo.Any(e => e.id == id);
        }

        // Metode koristene prilikom prikazivanja dostupnih vozila  
        public IActionResult dajDostupnaVozila()
        {
            var dostupnaVozila = _context.Vozilo
                .Where(v => v.dostupno == true)
                .ToList();

            return View("PonudaVozila", dostupnaVozila);
        }
        public IActionResult dajVozilaPoCijeni(string poredak = "asc")
        {
            List<Vozilo> sortiranaVozila;

            if (poredak == "desc")
            {
                sortiranaVozila = _context.Vozilo
                    .Where(v => v.dostupno == true)
                    .OrderByDescending(v => v.cijenaPoDanu)
                    .ToList();
            }
            else
            {
                sortiranaVozila = _context.Vozilo
                    .Where(v => v.dostupno == true)
                    .OrderBy(v => v.cijenaPoDanu)
                    .ToList();
            }

            ViewBag.SelektovaniPoredak = poredak;
            return View("PonudaVozila", sortiranaVozila);
        }
        public IActionResult dajVozilaPoTipu(string tip)
        {
            var filtrirani = _context.Vozilo
                .Where(v => v.dostupno && v.tip == tip)
                .ToList();
            ViewBag.SelektovaniTip = tip;
            return View("PonudaVozila", filtrirani);
        }
        public IActionResult Pretraga(string query)
        {
            List<Vozilo> rezultat;

            if (string.IsNullOrWhiteSpace(query))
            {
                rezultat = _context.Vozilo
                    .Where(v => v.dostupno == true)
                    .ToList();
            }
            else
            {
                rezultat = _context.Vozilo
                    .Where(v => v.model.ToLower().Contains(query.ToLower()))
                    .ToList();
            }

            ViewBag.query = query;
            return View("PonudaVozila", rezultat);
        }

        public IActionResult Rezervacija(List<int> vozilaId)
        {
            var dostupnaVozila = _context.Vozilo
                .Where(v => vozilaId.Contains(v.id))
                .ToList();

            ViewBag.OdabranaVozila = dostupnaVozila;
            return View();
        }


         
    }
}
