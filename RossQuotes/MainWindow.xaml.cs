using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Hardcodet.Wpf.TaskbarNotification;

namespace RossQuotes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Constants
        private const int MIN_INTERVAL = 1000; // millis
        private const int MAX_INTERVAL = 2000; // millis

        private const int MIN_MINS = 2;
        private const int MAX_MINS = 20;
        private const int PROB_SHOW_MESSAGE = 20; // chance out of 100 to show a message on a timer interval
        private static readonly string[] ROSS_QUOTES = {
            "We don't make mistakes. We just have happy accidents.",
            "Talent is a pursued interest. Anything that you're willing to practice, you can do.",
            "There's nothing wrong with having a tree as a friend.",
            "I guess I’m a little weird. I like to talk to trees and animals. That’s okay though; I have more fun than most people.",
            "Let's get crazy.",
            "I can't think of anything more rewarding than being able to express yourself to others through painting. Exercising the imagination, experimenting with talents, being creative; these things, to me, are truly the windows to your soul.",
            "Beat the devil out of it.",
            "I started painting as a hobby when I was little. I didn't know I had any talent. I believe talent is just a pursued interest. Anybody can do what I do.",
            "You have unlimited power on this canvas.",
            "Lets build a happy little cloud. Lets build some happy little trees.",
            "This is your world.",
            "I like to beat the brush.",
            "In painting, you have unlimited power. You have the ability to move mountains. You can bend rivers.",
            "You need the dark in order to show the light.",
            "Look around. Look at what we have. Beauty is everywhere - you only have to look to see it.",
            "Just go out and talk to a tree. Make friends with it.",
            "The secret to doing anything is believing that you can do it. Anything that you believe you can do strong enough, you can do. Anything. As long as you believe.",
            "I really believe that if you practice enough you could paint the 'Mona Lisa' with a two-inch brush."
        };
        private static readonly string[] RICK_STEVES_QUOTES = { };
        private static readonly string[] ARNOLD_QUOTES = { };
        private static readonly string[] DWIGHT_QUOTES = { };
        private static readonly string[] CHUCK_NORRIS_QUOTES = { };


        // Instance Variables
        Random randy;
        System.Timers.Timer timer;
        TaskbarIcon tbi;
        NotificationManager mNotificationManager;
        private string sender;
        private string message;

        // Constructor
        public MainWindow()
        {
            InitializeComponent();

            // initialize stuff
            randy = new Random();
            StartNewTimer(0, MAX_INTERVAL);
            mNotificationManager = new NotificationManager();

            //SetUpTimer();
            sender = "";
            message = "";

            // TODO initialize the TaskbarIcon
            tbi = new TaskbarIcon();
            tbi.ToolTipText = "Hello TaskbarIcon World!";
                        

            // Show custom balloon
            // TODO Create a custom UIElement that will be the UI for the notification
            //  then call tbi.ShowCustomBalloon(myUiElement, PopupAnimation, duration);
            NotificationUserControl notification = new NotificationUserControl();

            //tbi.ShowCustomBalloon(notification, System.Windows.Controls.Primitives.PopupAnimation.None, 4000);
        }


        // Event Handlers

        // When the window first loads
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        // On the timer interval
        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            // Show a notification on the app thread
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                ShowNotification();
            });

            // restart timer
            timer.Stop();
            timer.Dispose();
            //StartNewTimer(MIN_INTERVAL, MAX_INTERVAL);
        }


        // Methods

        private void StartNewTimer(int minInterval, int maxInterval)
        {
            timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            //timer.Interval = rand.Next(MIN_MINS * 60 * 1000, MAX_MINS * 60 * 1000);
            timer.Interval = randy.Next(minInterval, maxInterval);
            timer.Enabled = true;
        }

        private void ShowNotification()
        {
            NotificationModel notification = mNotificationManager.GetRandomNotificationModel();
            string title = notification.Sender;
            string text = notification.Message;
            System.Drawing.Icon icon = notification.Icon;

            // Show standard balloon with custom icon
            tbi.ShowBalloonTip(title, text, icon, true);
        }

        private void ShowPopup()
        {
            // instantiate new popup window
            PopupWindow popup = new PopupWindow();

            // set sender
            sender = "Bob Ross";

            // get random message
            message = GetRandomMessage(sender);

            // set popup texts
            popup.SetMainText(sender);
            popup.SetMessageText(message);

            // show popup
            popup.Show();
        }

        // returns a random quote based on sender
        private string GetRandomMessage(string senderIn)
        {
            string result = "";

            switch (senderIn)
            {
                case "Bob Ross":
                    result = ROSS_QUOTES[randy.Next(ROSS_QUOTES.Length)];
                    break;

                case "Rick Steves":
                    break;

                case "Arnold":
                    break;

                case "Dwight Schrute":
                    break;

                case "Chuck Norris":
                    break;
            }
            return result;
        }
    }
}
