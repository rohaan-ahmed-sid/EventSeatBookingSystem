using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers
{
    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payment/ProcessPayment/5
        public ActionResult ProcessPayment(int bookingId)
        {
            var booking = db.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking); // Display payment form
        }

        // POST: Payment/ConfirmPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmPayment(int bookingId, decimal amount, string paymentStatus, string paymentGateway)
        {
            var booking = db.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking == null)
            {
                return HttpNotFound();
            }

            var payment = new PaymentModel
            {
                BookingId = bookingId,
                Amount = amount,
                PaymentStatus = paymentStatus,
                PaymentGateway = paymentGateway,
                PaymentDate = System.DateTime.Now
            };

            db.Payments.Add(payment);
            db.SaveChanges();
            return RedirectToAction("PaymentSuccess", new { paymentId = payment.PaymentId });
        }

        // GET: Payment/PaymentSuccess
        public ActionResult PaymentSuccess(int paymentId)
        {
            var payment = db.Payments.FirstOrDefault(p => p.PaymentId == paymentId);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment); // Display payment success page
        }
    }
}
