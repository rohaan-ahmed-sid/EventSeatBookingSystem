using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        private EventSeatBookingSystemEntities db = new EventSeatBookingSystemEntities();

        public ActionResult Index()
        {
            var events = db.Events.OrderBy(e => e.Date).Take(5).ToList(); 
            return View(events); 
        }

        public ActionResult EventDetails(int id)
        {
            var eventDetails = db.Events.FirstOrDefault(e => e.EventId == id);
            if (eventDetails == null)
            {
                return HttpNotFound();
            }
            return View(eventDetails);
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    PasswordHash = model.PasswordHash, 
                    Role = "User",
                    CreatedAt = System.DateTime.Now
                };

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login", "User"); 
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email);
            if (user != null && user.PasswordHash !=null)
            {
                return RedirectToAction("Index", "Home"); 
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(); 
        }
    }
}
