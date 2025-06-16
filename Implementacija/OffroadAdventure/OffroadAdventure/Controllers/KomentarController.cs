using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OffroadAdventure.Models;
using OffroadAdventure.Models.Enums;

namespace OffroadAdventure.Controllers
{
    public class KomentarController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public KomentarController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Komentars
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Komentar.ToListAsync());
        }

        // GET: Komentars/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komentar = await _context.Komentar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (komentar == null)
            {
                return NotFound();
            }

            return View(komentar);
        }

        // POST: Komentars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,komentarId,ocjena,tekst,datum")] Komentar komentar)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid(); 
            }

            komentar.autor_id = user.Id;

            if (ModelState.IsValid)
            {
                _context.Add(komentar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(komentar);
        }


        // GET: Komentars/Edit/5
        [Authorize(Roles = "Administrator")]

        // POST: Komentars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,autor_id,komentarId,ocjena,tekst,datum")] Komentar komentar)
        {
            if (id != komentar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(komentar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KomentarExists(komentar.Id))
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
            return View(komentar);
        }

        // GET: Komentars/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komentar = await _context.Komentar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (komentar == null)
            {
                return NotFound();
            }

            return View(komentar);
        }

        // POST: Komentars/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var komentar = await _context.Komentar.FindAsync(id);
            if (komentar != null)
            {
                _context.Komentar.Remove(komentar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KomentarExists(int id)
        {
            return _context.Komentar.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Recenzije(string poredak = "")
        {
            var komentari = await _context.Komentar
                .Include(k => k.user)
                .ToListAsync();

            if (poredak == "asc")
                komentari = komentari.OrderBy(k => k.ocjena).ToList();
            else if (poredak == "desc")
                komentari = komentari.OrderByDescending(k => k.ocjena).ToList();

            ViewBag.Poredak = poredak;

            var recenzije = komentari.Where(k => k.komentarId == 0 && k.ocjena > 0).ToList();
            ViewBag.ProsjecnaOcjena = recenzije.Any() ? recenzije.Average(k => k.ocjena) : 0;
            ViewBag.BrojKomentara = komentari.Count;

            return View(komentari);
        }

        // POST: /Komentar/Dodaj
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Dodaj(int Ocjena, string Tekst)
        {
            if (string.IsNullOrWhiteSpace(Tekst) || Ocjena < 1 || Ocjena > 5)
            {
                return RedirectToAction("Recenzije", "Komentar");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Recenzije", "Komentar");
            }

            var komentar = new Komentar
            {
                tekst = Tekst,
                ocjena = Ocjena,
                datum = DateTime.Now,
                autor_id = user.Id
            };

            _context.Komentar.Add(komentar);
            await _context.SaveChangesAsync();

            return RedirectToAction("Recenzije", "Komentar");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Obrisi(int id)
        {
            var komentar = await _context.Komentar.FindAsync(id);
            var userId = _userManager.GetUserId(User);

            if (komentar == null || komentar.autor_id != userId)
            {
                return Forbid();
            }

            _context.Komentar.Remove(komentar);
            await _context.SaveChangesAsync();

            return RedirectToAction("Recenzije", "Komentar"); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Odgovori(int komentarId, string tekstOdgovora)
        {
            if (string.IsNullOrWhiteSpace(tekstOdgovora) || tekstOdgovora.Length < 2)
            {
                return RedirectToAction("Recenzije");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Recenzije");
            }

            var odgovor = new Komentar
            {
                tekst = tekstOdgovora,
                ocjena = 0,
                datum = DateTime.Now,
                autor_id = user.Id,
                komentarId = komentarId
            };

            _context.Komentar.Add(odgovor);
            await _context.SaveChangesAsync();

            var parentKomentar = await _context.Komentar
                .FirstOrDefaultAsync(k => k.Id == komentarId);

            if (parentKomentar != null && parentKomentar.autor_id != user.Id)
            {
                var notifikacija = new Notifikacija
                {
                    primalac_id = parentKomentar.autor_id,
                    tekst = $"Korisnik {user.Ime + " " + user.Prezime} je odgovorio na vaš komentar.",
                    datum = DateTime.Now,
                    status = StatusNotifikacije.NEPROCITANA
                };

                _context.Notifikacija.Add(notifikacija);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Recenzije");
        }



    }
}
