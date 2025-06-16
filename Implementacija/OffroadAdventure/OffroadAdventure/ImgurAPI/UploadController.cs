using Microsoft.AspNetCore.Mvc;

namespace OffroadAdventure.ImgurAPI
{
    public class UploadController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Imgur(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("Nema slike.");

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Client-ID", "9dfaafcfba67390"); 

            using var form = new MultipartFormDataContent();
            using var stream = image.OpenReadStream();
            var streamContent = new StreamContent(stream);
            form.Add(streamContent, "image", image.FileName);

            var response = await client.PostAsync("https://api.imgur.com/3/image", form);
            var json = await response.Content.ReadAsStringAsync();

            return Content(json, "application/json");
        }
    }
}
