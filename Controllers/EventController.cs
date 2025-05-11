using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers
{
    public class EventController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Event/Index
        public ActionResult Index()
        {
            var events = db.Events.ToList();
            return View(events);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventModel model)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Event/Details/5
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
