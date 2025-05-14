using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers.API
{
    [RoutePrefix("api/bookings")]
    public class BookingsApiController : ApiController
    {
        private EventSeatBookingSystemEntities db = new EventSeatBookingSystemEntities();

        // GET: api/bookings
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetBookings(int? userId = null)
        {
            var query = db.Bookings.AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(b => b.UserId == userId.Value);
            }

            var bookings = query.Select(b => new {
                b.BookingId,
                b.EventId,
                b.UserId,
                EventName = b.Event.Name,
                b.SeatId,
                SeatNumber = b.Seat.SeatNumber,
                b.BookingDate,
                b.Status,
                b.TotalAmount
            }).ToList();

            return Ok(bookings);
        }

        // POST: api/bookings
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateBooking([FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var seat = db.Seats.Find(booking.SeatId);
            if (seat == null || seat.IsAvailable == false)
            {
                return BadRequest("Seat not available");
            }

            // Update seat availability
            seat.IsAvailable = false;

            // Set booking date
            booking.BookingDate = DateTime.Now;
            booking.Status = "Confirmed";

            db.Bookings.Add(booking);
            db.SaveChanges();

            return Ok(new
            {
                booking.BookingId,
                booking.EventId,
                booking.UserId,
                booking.SeatId,
                booking.BookingDate,
                booking.Status,
                booking.TotalAmount
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}