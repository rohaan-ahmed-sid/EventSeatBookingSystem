using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenAI_API;
using System.Web.Http;

namespace EventSeatBookingSystem.Controllers
{
    [RoutePrefix("api/AI")]
    public class AIDecorSuggestionsController : ApiController
    {
        private readonly OpenAIAPI _openAi;

        public AIDecorSuggestionsController()
        {
            _openAi = new OpenAIAPI("api key"); 
        }

        [HttpPost]
        [Route("DecorSuggestions")]
        public IHttpActionResult GetDecorSuggestions([FromBody] DecorSuggestionsRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.EventName))
            {
                return BadRequest("Invalid input.");
            }

            var decorSuggestion = GetDecorSuggestionFromAI(request.EventName, request.Theme);
            
            return Ok(decorSuggestion); 
        }

        private string GetDecorSuggestionFromAI(string eventName, string theme)
        {
            var prompt = $"Suggest event decor ideas and layout for a {eventName} with the theme '{theme}'.";

            var completionRequest = _openAi.Completions.CreateCompletionAsync(prompt, model: "text-davinci-003", max_tokens: 150).Result;

            return completionRequest.Choices[0].Text.Trim(); 
        }
    }

    public class DecorSuggestionsRequest
    {
        public string EventName { get; set; } 
        public string Theme { get; set; }   
    }
}
