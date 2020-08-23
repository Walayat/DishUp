using DishUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DishUp.Filters
{
    public class NotificationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var context = new dishupEntities();

            var notifications = context.NOTIFICATIONs.GroupBy(n => n.ID_NOTIFICATION_TYPE)
    .Select(g => new NotificationViewModel
    {
        Count = g.Count(),
        NotificationType = g.Key.ToString(),
        //BadgeClass = NotificationType.Email == Convert.ToInt16((g.Key) ? "success" : "info"
    });

            filterContext.Controller.ViewBag.Notifications = notifications;

        }
    }
}