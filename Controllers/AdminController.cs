using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Dashboard - Displays admin dashboard with system statistics
        public ActionResult Dashboard()
        {
            var totalUsers = db.Users.Count();
            var totalEvents = db.Events.Count();
            var totalBookings = db.Bookings.Count();
            var totalRevenue = db.Payments.Sum(p => p.Amount);
            var totalSeatsAvailable = db.Seats.Where(s => s.IsAvailable == 1).Count();
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

            return View(stats); // Return statistics to the dashboard view
        }

        // GET: Admin/ManageUsers - Displays a list of all users
        public ActionResult ManageUsers()
        {
            var users = db.Users.ToList();
            return View(users); // Display a list of all users
        }

        // GET: Admin/ManageEvents - Displays a list of all events
        public ActionResult ManageEvents()
        {
            var events = db.Events.ToList();
            return View(events); // Display a list of all events
        }

        // GET: Admin/ManageBookings - Displays all bookings
        public ActionResult ManageBookings()
        {
            var bookings = db.Bookings.Include("User").Include("Event").Include("Seat").ToList();
            return View(bookings); // Display all bookings with associated data
        }

        // GET: Admin/ManagePayments - Displays all payments
        public ActionResult ManagePayments()
        {
            var payments = db.Payments.Include("Booking").ToList();
            return View(payments); // Display a list of all payments
        }
    }
}
