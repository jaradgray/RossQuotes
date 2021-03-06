﻿using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace RossQuotes
{
    /// <summary>
    /// Interaction logic for App.xaml. Check the XAML for comments.
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon mNotifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize the notifyicon (it's a resource declared in NotifyIconResources.xaml
            mNotifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            mNotifyIcon.DataContext = new NotifyIconViewModel();
            mNotifyIcon.Icon = RossQuotes.Properties.Resources.icon_app_blue; // the icon that will be displayed in the system tray

            // Create a NotificationSerbice to start displaying notifications
            new NotificationService(mNotifyIcon);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            mNotifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }
    }
}
