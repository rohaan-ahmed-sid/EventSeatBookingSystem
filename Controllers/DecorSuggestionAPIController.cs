using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace EventSeatBookingSystem.Controllers
{
    [RoutePrefix("api/AI")]
    public class DecorSuggestionAPIController : ApiController
    {
        [HttpPost]
        [Route("DecorSuggestionsImage")]
        public async Task<IHttpActionResult> GetDecorSuggestionImage([FromBody] DecorSuggestionsRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.EventName))
            {
                return BadRequest("Invalid input.");
            }

            string prompt = $"Event: {request.EventName}, Theme: {request.Theme}. Generate a beautiful event decor idea.";
            string imageUrl = await GenerateImageWithHuggingFace(prompt);
            return Ok(new { imageUrl });
        }

        private async Task<string> GenerateImageWithHuggingFace(string prompt)
        {
            string apiToken = "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";
            string endpoint = "https://api-inference.huggingface.co/models/stabilityai/stable-diffusion-2-1";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);
                var content = new StringContent("{\"inputs\": \"" + prompt + "\"}", System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                var bytes = await response.Content.ReadAsByteArrayAsync();

                string folderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/GeneratedImages/");
                if (!System.IO.Directory.Exists(folderPath))
                    System.IO.Directory.CreateDirectory(folderPath);

                string fileName = Guid.NewGuid().ToString() + ".png";
                string filePath = System.IO.Path.Combine(folderPath, fileName);
                System.IO.File.WriteAllBytes(filePath, bytes);

                string imageUrl = "/GeneratedImages/" + fileName;
                return imageUrl;
            }
        }
    }

    public class DecorSuggestionsRequest
    {
        public string EventName { get; set; }
        public string Theme { get; set; }
    }
}