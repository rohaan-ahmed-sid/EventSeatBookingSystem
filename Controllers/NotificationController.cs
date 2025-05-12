using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventSeatBookingSystem.Models;

namespace EventSeatBookingSystem.Controllers
{
    public class NotificationController : Controller
    {
        private EventSeatBookingSystemEntities db = new EventSeatBookingSystemEntities();

        public ActionResult SendNotification(int userId, int eventId, string message, string type)
        {
            var notification = new Notification
            {
                UserId = userId,
                EventId = eventId,
                Message = message,
                NotificationDate = System.DateTime.Now,
                Type = type
            };

            db.Notifications.Add(notification);
            db.SaveChanges();
            return RedirectToAction("NotificationSent", new { notificationId = notification.NotificationId });
        }

        public ActionResult NotificationSent(int notificationId)
        {
            var notification = db.Notifications.FirstOrDefault(n => n.NotificationId == notificationId);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification); 
        }
    }
}
