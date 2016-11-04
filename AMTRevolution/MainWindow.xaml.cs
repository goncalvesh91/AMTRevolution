using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AMTRevolution
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region bttActions
        private void exitBtt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void maxiBtt_Click(object sender, RoutedEventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Maximized: Application.Current.MainWindow.WindowState = WindowState.Normal; break;
                case WindowState.Normal: Application.Current.MainWindow.WindowState = WindowState.Maximized; break;
            }
        }

        private void minBtt_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void mainMenuBtt_Click(object sender, RoutedEventArgs e)
        {
            switch(mainMenuPopup.IsOpen)
            {
                case true: mainMenuPopup.IsOpen = false;break;
                case false: mainMenuPopup.IsOpen = true; break;
            }
        }

        private void settingsBtt_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void aboutBtt_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void searchSiteBtt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void newTemplateBtt_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

    }
}
