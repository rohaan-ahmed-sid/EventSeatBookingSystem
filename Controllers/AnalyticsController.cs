using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers
{
    public class AnalyticsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Analytics/EventStats
        public ActionResult EventStats(int eventId)
        {
            var eventStats = db.Analytics.FirstOrDefault(a => a.EventId == eventId);
            if (eventStats == null)
            {
                return HttpNotFound();
            }
            return View(eventStats); // Show analytics data for the event
        }
    }
}
