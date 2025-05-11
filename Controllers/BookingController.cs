using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Booking/Book/5
        public ActionResult Book(int eventId)
        {
            var eventDetails = db.Events.FirstOrDefault(e => e.EventId == eventId);
            var availableSeats = db.Seats.Where(s => s.EventId == eventId && s.IsAvailable == 1).ToList();
            if (eventDetails == null || !availableSeats.Any())
            {
                return HttpNotFound();
            }
            return View(new BookingViewModel { Event = eventDetails, AvailableSeats = availableSeats });
        }

        // POST: Booking/Book/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Book(int eventId, int seatId)
        {
            var seat = db.Seats.FirstOrDefault(s => s.SeatId == seatId && s.IsAvailable == 1);
            if (seat == null)
            {
                ModelState.AddModelError("", "Selected seat is not available.");
                return RedirectToAction("Book", new { eventId = eventId });
            }

            seat.IsAvailable = 0; // Mark the seat as booked
            var booking = new BookingModel
            {
                UserId = 1, // Should be replaced by current logged-in user's ID
                EventId = eventId,
                SeatId = seatId,
                Status = "Booked"
            };

            db.Bookings.Add(booking);
            db.SaveChanges();
            return RedirectToAction("BookingSuccess", new { bookingId = booking.BookingId });
        }

        // GET: Booking/BookingSuccess
        public ActionResult BookingSuccess(int bookingId)
        {
            var booking = db.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }
    }
}
