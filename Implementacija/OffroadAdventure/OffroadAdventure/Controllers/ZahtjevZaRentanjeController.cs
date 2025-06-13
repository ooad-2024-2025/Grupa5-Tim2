using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OffroadAdventure.Data.Migrations;
using OffroadAdventure.Models;
using OffroadAdventure.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OffroadAdventure.Controllers
{
    public class ZahtjevZaRentanjeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZahtjevZaRentanjeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ZahtjevZaRentanje
                .Include(z => z.User)
                .Include(z => z.Stavke)
                    .ThenInclude(s => s.Vozilo);

            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var zahtjev = await _context.ZahtjevZaRentanje
                .Include(z => z.User)
                .Include(z => z.Stavke)
                    .ThenInclude(s => s.Vozilo)
                .FirstOrDefaultAsync(z => z.id == id);

            if (zahtjev == null) return NotFound();
            return View(zahtjev);
        }

        public IActionResult Create()
        {
            ViewData["korisnik_id"] = new SelectList(_context.Users, "Id", "Id");
            ViewBag.Statusi = Enum.GetValues(typeof(StatusZahtjeva))
           .Cast<StatusZahtjeva>()
           .Select(s => new SelectListItem
           {
               Text = s.ToString(),
               Value = ((int)s).ToString()
           }).ToList();
            ViewBag.SvaVozila = _context.Vozilo.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ZahtjevZaRentanje z, string akcija, List<int> vozilaId)
        {
            z.korisnik_id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ModelState.Remove(nameof(z.korisnik_id));
            ModelState.Remove(nameof(z.User));
            z.statusZahtjeva = StatusZahtjeva.NA_CEKANJU;
            if (string.IsNullOrWhiteSpace(z.dodatniZahtjev))
            {
                z.dodatniZahtjev = "";
                ModelState.Remove(nameof(z.dodatniZahtjev));
            }
            var vozila = await _context.Vozilo.Where(v => vozilaId.Contains(v.id)).ToListAsync();
            var (popustProcenat, cijenaSaPopustom) = IzracunajPopust(vozila, z.datumOd, z.datumDo);
            z.popust = popustProcenat;
            z.cijena = cijenaSaPopustom;

            _context.ZahtjevZaRentanje.Add(z);
            await _context.SaveChangesAsync();
            foreach (var vozilo in vozila)
            {
                var stavka = new StavkaZahtjeva
                {
                    ZahtjevZaRentanjeId = z.id,
                    VoziloId = vozilo.id
                };
                _context.StavkaZahtjeva.Add(stavka);
            }
            await _context.SaveChangesAsync();

            var placanje = new Placanje
            {
                zahtjevZaRentanjeId = z.id,
                nacinPlacanja = NacinPlacanja.GOTOVINA,
                statusPlacanja = StatusPlacanja.NA_CEKANJU,
                datumPlacanja = DateTime.Now,
                popust = z.popust
            };

            _context.Placanje.Add(placanje);
            await _context.SaveChangesAsync();

            if (akcija == "kartica")
            {
                ViewBag.ZahtjevId = z.id;
                return View("~/Views/Home/KarticnoPlacanje.cshtml");
            }
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
                    tekst = $"Korisnik {z.ime} {z.prezime} je izvršio rezervaciju za rentanje vozila.",
                    datum = DateTime.Now,
                    status = StatusNotifikacije.NEPROCITANA
                });
            }

            await _context.SaveChangesAsync();

            TempData["poruka"] = $"Zahtjev je uspješno kreiran! Očekujte obavještenje uskoro";
            return RedirectToAction("Rezervacija", "Vozilo");
        }

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
            ViewBag.Statusi = Enum.GetValues(typeof(StatusZahtjeva))
           .Cast<StatusZahtjeva>()
           .Select(s => new SelectListItem
           {
               Text = s.ToString(),
               Value = ((int)s).ToString()
           }).ToList();
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

        [HttpPost]
        public IActionResult OdboriZahtjev(int id)
        {
            var zahtjev = _context.ZahtjevZaRentanje.Find(id);
            if (zahtjev == null) return NotFound();
            zahtjev.statusZahtjeva = StatusZahtjeva.ODOBREN;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult OdbijZahtjev(int id)
        {
            var zahtjev = _context.ZahtjevZaRentanje.Find(id);
            if (zahtjev == null) return NotFound();
            zahtjev.statusZahtjeva = StatusZahtjeva.ODBIJEN;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public List<ZahtjevZaRentanje> DajZahtjeveKorisnika(int id)
        {
            return _context.ZahtjevZaRentanje.Where(z => z.User.Id == id.ToString()).ToList();
        }

        public List<ZahtjevZaRentanje> DajSveZahtjeve()
        {
            return _context.ZahtjevZaRentanje.ToList();
        }

        private bool ZahtjevZaRentanjeExists(int id)
        {
            return _context.ZahtjevZaRentanje.Any(e => e.id == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Odobri(int id)
        {
            var zahtjev = await _context.ZahtjevZaRentanje.Include(z => z.User).FirstOrDefaultAsync(z => z.id == id);
            if (zahtjev == null) return NotFound();

            zahtjev.statusZahtjeva = StatusZahtjeva.ODOBREN;
            _context.Update(zahtjev);

            if (zahtjev.korisnik_id != null)
            {
                _context.Notifikacija.Add(new Notifikacija
                {
                    primalac_id = zahtjev.korisnik_id,
                    tekst = "Vaš zahtjev za rentanje vozila je odobren.",
                    datum = DateTime.Now,
                    status = StatusNotifikacije.NEPROCITANA
                });
            }


            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Odbij(int id)
        {
            var zahtjev = await _context.ZahtjevZaRentanje.Include(z => z.User).FirstOrDefaultAsync(z => z.id == id);
            if (zahtjev == null) return NotFound();

            zahtjev.statusZahtjeva = StatusZahtjeva.ODBIJEN;
            _context.Update(zahtjev);

            if (zahtjev.korisnik_id != null)
            {
                _context.Notifikacija.Add(new Notifikacija
                {
                    primalac_id = zahtjev.korisnik_id,
                    tekst = "Nažalost, vaš zahtjev za rentanje vozila je odbijen.",
                    datum = DateTime.Now,
                    status = StatusNotifikacije.NEPROCITANA
                });
            }


            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private (double popust, double cijenaSaPopustom) IzracunajPopust(List<Vozilo> vozila, DateTime datumOd, DateTime datumDo)
        {
            int brojDana = (datumDo - datumOd).Days + 1;
            double osnovnaCijena = vozila.Sum(v => v.cijenaPoDanu) * brojDana;

            int brojVozila = vozila.Count;
            double popustProcenat = Math.Min((Math.Max(brojVozila - 1, 0) * 5 + Math.Max(brojDana - 1, 0) * 2), 50);
            double cijenaSa = osnovnaCijena * (1 - popustProcenat / 100);

            return (popustProcenat, cijenaSa);
        }



    }
}

