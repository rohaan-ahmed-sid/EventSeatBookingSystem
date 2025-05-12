using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers
{
    [RoutePrefix("api/SeatSelection")]
    public class SeatSelectionAPIController : ApiController
    {
        private EventSeatBookingSystemEntities db = new EventSeatBookingSystemEntities();  

        [HttpPost]
        [Route("SelectSeats")]
        public IHttpActionResult GetBestSeats([FromBody] SeatSelectionRequest request)
        {
            if (request == null || request.EventId <= 0)
            {
                return BadRequest("Invalid input.");
            }

            var eventDetails = db.Events.FirstOrDefault(e => e.EventId == request.EventId);
            if (eventDetails == null)
            {
                return NotFound();  
            }

            var availableSeats = db.Seats.Where(s => s.EventId == request.EventId && s.IsAvailable == 1).ToList();

            var bestSeats = SelectSeatsBasedOnUserPreferences(availableSeats, request);

            return Ok(bestSeats); 
        }

        private List<Seat> SelectSeatsBasedOnUserPreferences(List<Seat> availableSeats, SeatSelectionRequest request)
        {
            var recommendedSeats = availableSeats;

            if (!string.IsNullOrEmpty(request.SeatType))
            {
                recommendedSeats = recommendedSeats.Where(s => s.SeatType == request.SeatType).ToList();
            }

            if (request.ProximityToStage > 0)
            {
                recommendedSeats = recommendedSeats.Where(s => CalculateProximity(s.SeatNumber) <= request.ProximityToStage).ToList();
            }

            recommendedSeats = recommendedSeats.OrderBy(s => CalculateSeatScore(s, request)).Take(5).ToList();  

            return recommendedSeats;
        }

        private int CalculateProximity(string seatNumber)
        {
            return seatNumber.Length; 
        }

        private int CalculateSeatScore(Seat seat, SeatSelectionRequest request)
        {
            int score = 0;

            if (seat.SeatType == request.SeatType) score += 10;

            if (CalculateProximity(seat.SeatNumber) <= request.ProximityToStage) score += 5;

            return score;
        }
    }

    public class SeatSelectionRequest
    {
        public int EventId { get; set; }  
        public string SeatType { get; set; } 
        public int ProximityToStage { get; set; }  
    }
}
