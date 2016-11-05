using System;
using System.Windows;

namespace AMTRevolution
{
    public partial class splashScreen : Window
    {
        public splashScreen()
        {
            InitializeComponent();
        }

        private void myGif_MediaEnded(object sender, RoutedEventArgs e)
        {
            splashMedia.Position = new TimeSpan(0, 0, 1);
            splashMedia.Play();
        }

        internal string updateStatus
        {
            get { return statusLabel.Text.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { statusLabel.Text = value; })); }
        }
    }
}
