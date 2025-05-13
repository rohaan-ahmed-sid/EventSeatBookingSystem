using System.Linq;
using System.Web.Mvc;
using EventSeatBookingSystem.Models;
using Microsoft.AspNet.Identity;

namespace EventSeatBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private EventSeatBookingSystemEntities db = new EventSeatBookingSystemEntities();

        public ActionResult Book(int eventId)
        {
            var eventDetails = db.Events.FirstOrDefault(e => e.EventId == eventId);
            var availableSeats = db.Seats
                .Where(s => s.EventId == eventId && (s.IsAvailable == true || s.IsAvailable == null))
                .ToList();

            if (eventDetails == null || !availableSeats.Any())
            {
                return HttpNotFound();
            }

            var viewModel = new BookingViewModel
            {
                Event = eventDetails,
                AvailableSeats = availableSeats
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Book(int eventId, int seatId)
        {
            var seat = db.Seats.FirstOrDefault(s => s.SeatId == seatId && (s.IsAvailable == true || s.IsAvailable == null));
            if (seat == null)
            {
                ModelState.AddModelError("", "Selected seat is not available.");
                return RedirectToAction("Book", new { eventId = eventId });
            }

            seat.IsAvailable = false;

            var booking = new Booking
            {
                UserId = User.Identity.GetUserId<int>(), 
                EventId = eventId,
                SeatId = seatId,
                Status = "Booked",
                BookingDate = System.DateTime.Now
            };

            db.Bookings.Add(booking);
            db.SaveChanges();

            return RedirectToAction("BookingSuccess", new { bookingId = booking.BookingId });
        }

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

    public class BookingViewModel
    {
        public Event Event { get; set; }
        public List<Seat> AvailableSeats { get; set; }
    }
}
