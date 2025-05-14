using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers
{
    [RoutePrefix("api/admin/bookings")]
    public class AdminBookingsController : ApiController
    {
        private EventSeatBookingSystemEntities db = new EventSeatBookingSystemEntities();

        // GET: api/admin/bookings
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetBookings()
        {
            var bookings = db.Bookings.Select(b => new {
                b.BookingId,
                b.EventId,
                b.UserId,
                EventName = b.Event.Name,
                UserName = b.User.Username,
                b.SeatId,
                SeatNumber = b.Seat.SeatNumber,
                b.BookingDate,
                b.Status,
                b.TotalAmount
            }).ToList();

            return Ok(bookings);
        }
    }
}