using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers
{
    public class AdminController : Controller
    {
        private EventSeatBookingSystemEntities db = new EventSeatBookingSystemEntities();

        public ActionResult Dashboard()
        {
            var totalUsers = db.Users.Count();
            var totalEvents = db.Events.Count();
            var totalBookings = db.Bookings.Count();
            var totalRevenue = db.Payments.Sum(p => p.Amount);
            var totalSeatsAvailable = db.Seats.Where(s => (bool)s.IsAvailable).Count();
            var totalTicketsSold = db.Seats.Count() - totalSeatsAvailable;

            var stats = new
            {
                TotalUsers = totalUsers,
                TotalEvents = totalEvents,
                TotalBookings = totalBookings,
                TotalRevenue = totalRevenue,
                TotalSeatsAvailable = totalSeatsAvailable,
                TotalTicketsSold = totalTicketsSold
            };

            return View(stats); 
        }

        public ActionResult ManageUsers()
        {
            var users = db.Users.ToList();
            return View(users); 
        }

        public ActionResult ManageEvents()
        {
            var events = db.Events.ToList();
            return View(events); 
        }

        public ActionResult ManageBookings()
        {
            var bookings = db.Bookings.Include("User").Include("Event").Include("Seat").ToList();
            return View(bookings);
        }

        public ActionResult ManagePayments()
        {
            var payments = db.Payments.Include("Booking").ToList();
            return View(payments); 
        }
    }
}
