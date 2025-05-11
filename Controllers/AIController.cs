using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers
{
    public class AIController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AI/Recommend/5
        public ActionResult Recommend(int eventId)
        {
            var eventDetails = db.Events.FirstOrDefault(e => e.EventId == eventId);
            if (eventDetails == null)
            {
                return HttpNotFound();
            }

            // Placeholder AI logic for recommendations
            var recommendation = new AIRecommendationModel
            {
                EventId = eventId,
                RecommendedSeats = "Seats 1-10 are ideal for VIPs.",
                DecorSuggestion = "Use blue and gold colors for better ambiance."
            };

            db.AIRecommendations.Add(recommendation);
            db.SaveChanges();
            return View(recommendation); // Show AI recommendations
        }
    }
}
