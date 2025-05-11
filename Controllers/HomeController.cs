using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Home/Index - Displays a list of upcoming events
        public ActionResult Index()
        {
            var events = db.Events.OrderBy(e => e.Date).Take(5).ToList(); // Fetch upcoming 5 events
            return View(events); // Pass events to the homepage view
        }

        // GET: Home/EventDetails/5 - Displays details of a specific event
        public ActionResult EventDetails(int id)
        {
            var eventDetails = db.Events.FirstOrDefault(e => e.EventId == id);
            if (eventDetails == null)
            {
                return HttpNotFound(); // If event not found, return a 404
            }
            return View(eventDetails); // Display event details
        }

        // GET: Home/Register - Displays the registration form
        public ActionResult Register()
        {
            return View();
        }

        // POST: Home/Register - Handles the user registration form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserModel
                {
                    Name = model.Name,
                    Email = model.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.PasswordHash), // Hash password
                    Role = "User",
                    CreatedAt = System.DateTime.Now
                };

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login", "User"); // Redirect to login after registration
            }
            return View(model); // Return to the form with validation errors
        }

        // GET: Home/Login - Displays the login form
        public ActionResult Login()
        {
            return View();
        }

        // POST: Home/Login - Handles the login form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                // Store user session or handle login here
                return RedirectToAction("Index", "Home"); // Redirect to home page on successful login
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(); // Return to login page with error
        }
    }
}
