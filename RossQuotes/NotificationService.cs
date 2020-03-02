using System;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace RossQuotes
{
    /// <summary>
    /// This class the logic of scheduling and displaying notifications.
    /// </summary>
    public class NotificationService
    {
        // Constants
        private const int MIN_INTERVAL = 5000; // millis
        private const int MAX_INTERVAL = 7000; // millis


        // Instance Variables

        private TaskbarIcon mNotifyIcon;
        private Random mRandy;
        private System.Timers.Timer mTimer;
        private NotificationManager mNotificationManager;


        // Constructor

        public NotificationService(TaskbarIcon notifyIcon)
        {
            mNotifyIcon = notifyIcon;

            // Initialize stuff
            mRandy = new Random();
            StartNewTimer(0, MAX_INTERVAL);
            mNotificationManager = new NotificationManager();
        }


        // API methods




        // Private methods

        /// <summary>
        /// Re-inits mTimer, sets its interval to a random value between @minInterval and @maxInterval,
        /// and enables it.
        /// </summary>
        /// <param name="minInterval"></param>
        /// <param name="maxInterval"></param>
        private void StartNewTimer(int minInterval, int maxInterval)
        {
            mTimer = new System.Timers.Timer();
            mTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            mTimer.Interval = mRandy.Next(minInterval, maxInterval);
            mTimer.Enabled = true;
        }

        /// <summary>
        /// Handler for mTimer's Elapsed event. Shows a notification, then kills mTimer.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            // Show a notification on the app thread
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                ShowNotification();
            });

            // Kill timer
            mTimer.Stop();
            mTimer.Dispose();

            // Reset timer to show a new notification some time between MIN_INTERVAL and MAX_INTERVAL millis from now
            StartNewTimer(MIN_INTERVAL, MAX_INTERVAL);
        }

        /// <summary>
        /// Gets a random NotificationModel from mNotificationManager and displays its data
        /// as a bolloon tip (notification).
        /// </summary>
        private void ShowNotification()
        {
            NotificationModel notification = mNotificationManager.GetRandomNotificationModel();
            string title = notification.Sender;
            string text = notification.Message;
            System.Drawing.Icon icon = notification.Icon;

            // Show standard balloon with custom icon
            mNotifyIcon.ShowBalloonTip(title, text, icon, true);
        }


    }
}
