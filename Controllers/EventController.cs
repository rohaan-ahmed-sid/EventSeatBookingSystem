using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers
{
    public class EventController : Controller
    {
        private EventSeatBookingSystemEntities db = new EventSeatBookingSystemEntities();

        public ActionResult Index()
        {
            var events = db.Events.ToList();
            return View(events);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Event model)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var eventDetails = db.Events.FirstOrDefault(e => e.EventId == id);
            if (eventDetails == null)
            {
                return HttpNotFound();
            }
            return View(eventDetails);
        }
    }
}
