using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OffroadAdventure.Models;

namespace OffroadAdventure.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PonudaVozila()
        {
            return RedirectToAction("dajDostupnaVozila", "Vozilo");
        }

        public IActionResult SaznajteVise()
        {
            return View();
        }
        public IActionResult Rezervacija()
        {
            return View();
        }
        public IActionResult ONama()
        {
            return View();
        }
        public IActionResult Recenzije()
        {
            return View();
        }
        public IActionResult KarticnoPlacanje()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
