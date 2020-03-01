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
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace RossQuotes
{
    /// <summary>
    /// Interaction logic for PopupWindow.xaml
    /// </summary>
    public partial class PopupWindow : Window
    {
        // Constants
        private const int TIMEOUT_MILLIS = 500;


        // Instance Variables
        System.Timers.Timer timoutTimer;


        // Constructor
        public PopupWindow()
        {
            InitializeComponent();

            // set up timer
            timoutTimer = new System.Timers.Timer();
            timoutTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimeout);
            timoutTimer.Interval = TIMEOUT_MILLIS;
            timoutTimer.Enabled = true;
        }


        // Event Handlers

        private void OnTimeout (object sender, System.Timers.ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                FadeOutWindow();
            });
        }

        private void popupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            popupWindow.Left = SystemParameters.PrimaryScreenWidth;
            popupWindow.Top = SystemParameters.PrimaryScreenHeight - popupWindow.Height - 50;
        }

        // Hover over close "x"
        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            closeX.Foreground = new SolidColorBrush(Color.FromRgb(0xe6, 0xe6, 0xe6));
        }

        // Mouse leave close "x"
        private void closeX_MouseLeave(object sender, MouseEventArgs e)
        {
            closeX.Foreground = new SolidColorBrush(Color.FromRgb(0x9e, 0x9e, 0x9e));
        }

        // Mouse down on close "x"
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FadeOutWindow();
        }

        private void OnFadeOutComplete(object sender, EventArgs e)
        {
            this.Close();
        }


        // Methods

        public void SetMainText(string text)
        {
            mainText.Text = text;
        }

        public void SetMessageText(string text)
        {
            messageText.Text = text;
        }

        private void FadeOutWindow()
        {
            // create animation
            DoubleAnimation opacityAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(1));

            // add completed event handler
            opacityAnimation.Completed += new EventHandler(OnFadeOutComplete);

            // begin animation
            popupWindow.BeginAnimation(Window.OpacityProperty, opacityAnimation);
        }

        
    }
}
