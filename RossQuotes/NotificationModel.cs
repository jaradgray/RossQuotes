using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossQuotes
{
    /// <summary>
    /// Holds data to be displayed in a notification.
    /// </summary>
    public class NotificationModel
    {
        /// <summary>
        /// The name of the notification's ficticious sender.
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// The message the notification will display.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The Icon that will be displayed with the notification.
        /// </summary>
        public System.Drawing.Icon Icon { get; set; }
    }
}
