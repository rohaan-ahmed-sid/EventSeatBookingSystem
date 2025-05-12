using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers
{
    public class AnalyticsController : Controller
    {
        private EventSeatBookingSystemEntities db = new EventSeatBookingSystemEntities();

        public ActionResult EventStats(int eventId)
        {
            var eventStats = db.Analytics.FirstOrDefault(a => a.EventId == eventId);
            if (eventStats == null)
            {
                return HttpNotFound();
            }
            return View(eventStats);
        }
    }
}
