using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using OffroadAdventure.OpenRouteServiceAPI;

namespace OffroadAdventure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LokacijaController : ControllerBase
    {
        private readonly OpenRouteServiceClient _ors;

        public LokacijaController(OpenRouteServiceClient ors)
        {
            _ors = ors;
        }

        [HttpPost("putanja")]
        public async Task<IActionResult> GetRuta([FromBody] GradDTO dto)
        {
            if (!GradKoordinate.Gradovi.TryGetValue(dto.Grad, out var koordinate))
                return BadRequest("Nepoznat grad");

            try
            {
                var result = await _ors.GetRutaAsync(
                    koordinate.Lon, koordinate.Lat,
                    18.7012, 43.3705);

                var jsonDoc = JsonDocument.Parse(result);
                return Ok(jsonDoc.RootElement);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greška u API pozivu: " + ex.Message);
                return StatusCode(500, new { error = "Greška prilikom traženja rute.", detalji = ex.Message });
            }
        }

    }
}
