using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.RD.BoraNow.PresentationLayer.WebAPI.Models.HtmlComponents
{
    public class AlertNotification
    {
        public string Notification { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
    }
}
