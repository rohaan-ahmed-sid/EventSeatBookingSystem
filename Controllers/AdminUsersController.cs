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
    [RoutePrefix("api/admin/users")]
    public class AdminUsersController : ApiController
    {
        private EventSeatBookingSystemEntities db = new EventSeatBookingSystemEntities();

        // GET: api/admin/users
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetUsers()
        {
            var users = db.Users.Select(u => new {
                u.UserId,
                u.Username,
                u.Email,
                u.FirstName,
                u.LastName,
                u.Role
            }).ToList();

            return Ok(users);
        }
    }
}