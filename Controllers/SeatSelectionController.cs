using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers
{
    [System.Web.Http.RoutePrefix("api/seatselection")]
    public class SeatSelectionController : ApiController
    {
        private EventSeatBookingSystemEntities db = new EventSeatBookingSystemEntities();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("SelectSeats")]
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

            var availableSeats = db.Seats.Where(s => s.EventId == request.EventId && s.IsAvailable.HasValue && s.IsAvailable.Value).ToList();

            var rankedSeats = RankSeatsBasedOnPreferences(availableSeats, request);

            return Ok(rankedSeats.Take(5));
        }

        private List<Seat> RankSeatsBasedOnPreferences(List<Seat> availableSeats, SeatSelectionRequest request)
        {
            List<RankedSeat> rankedSeats = new List<RankedSeat>();

            foreach (var seat in availableSeats)
            {
                float score = 0;

                if (!string.IsNullOrEmpty(request.SeatType) && seat.SeatType == request.SeatType)
                {
                    score += 10;
                }

                if (request.ProximityToStage > 0)
                {
                    int distanceToStage = CalculateProximity(seat.SeatNumber);
                    if (distanceToStage <= request.ProximityToStage)
                    {
                        score += 5;
                    }
                }

                rankedSeats.Add(new RankedSeat { Seat = seat, Score = score });
            }

            var sortedSeats = rankedSeats.OrderByDescending(r => r.Score).Select(r => r.Seat).ToList();
            return sortedSeats;
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

    public class RankedSeat
    {
        public Seat Seat { get; set; }
        public float Score { get; set; }
    }

}