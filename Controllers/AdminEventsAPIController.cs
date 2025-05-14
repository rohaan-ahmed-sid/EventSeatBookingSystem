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
	[RoutePrefix("api/admin/events")]
	public class AdminEventsAPIController : ApiController
	{
		private EventSeatBookingSystemEntities db = new EventSeatBookingSystemEntities();

		// GET: api/admin/events
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
	}
}