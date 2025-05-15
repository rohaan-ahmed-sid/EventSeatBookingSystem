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
    [RoutePrefix("/api/events")]
    public class EventsController : ApiController
    {
        private EventSeatBookingSystemEntities db = new EventSeatBookingSystemEntities();

        // GET: api/events
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetEvents()
        {
            var events = db.Events.Select(e => new {
                e.EventId,
                e.Name,
                e.Description,
                e.EventDate,
                e.Venue,
                e.TotalSeats,
                e.AvailableSeats,
                e.TicketPrice,
                e.ImageUrl
            }).ToList();

            return Ok(events);
        }

        // GET: api/events/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetEvent(int id)
        {
            var eventItem = db.Events.Find(id);
            if (eventItem == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                eventItem.EventId,
                eventItem.Name,
                eventItem.Description,
                eventItem.EventDate,
                eventItem.Venue,
                eventItem.TotalSeats,
                eventItem.AvailableSeats,
                eventItem.TicketPrice,
                eventItem.ImageUrl
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